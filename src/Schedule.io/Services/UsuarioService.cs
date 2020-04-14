using MediatR;
using Schedule.io.Core.Communication.Mediator;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Interfaces.Services;
using Schedule.io.Interfaces;
using Schedule.io.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Events.UsuarioEvents;

namespace Schedule.io.Services
{
    internal class UsuarioService : ServiceBase, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository,
                              IMediatorHandler bus,
                              IUnitOfWork uow,
                              INotificationHandler<DomainNotification> notifications) : base(bus, uow, notifications)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void Gravar(Usuario usuario)
        {
            var usuarioQuery = _usuarioRepository.Obter(usuario.Id);
            if (usuarioQuery == null)
                Registrar(usuario);
            else
                Atualizar(usuario);

            ValidarComando();
        }

        public Usuario Obter(string usuarioId)
        {
            return _usuarioRepository.Obter(usuarioId);
        }

        public IEnumerable<Usuario> Listar()
        {
            var usuarios = _usuarioRepository.Listar();
            foreach (var usuario in usuarios)
            {
                yield return usuario;
            }
        }

        public void Excluir(string usuarioId)
        {
            var usuario = _usuarioRepository.Obter(usuarioId);
            if (usuario == null)
            {
                _bus.PublicarNotificacao(new DomainNotification("", "Usuario não encontrado!"));
                ValidarComando();
            }

            _usuarioRepository.Excluir(usuario);

            if (Commit())
                _bus.PublicarEvento(new UsuarioRemovidoEvent(usuario.Id)).Wait();

            ValidarComando();
        }

        #region Privados
        private void Registrar(Usuario usuario)
        {
            _usuarioRepository.Adicionar(usuario);

            if (Commit())
                _bus.PublicarEvento(new UsuarioRegistradoEvent(usuario.Id, usuario.Email));
        }

        private void Atualizar(Usuario usuario)
        {
            _usuarioRepository.Atualizar(usuario);

            if (Commit())
                _bus.PublicarEvento(new UsuarioAtualizadoEvent(usuario.Id, usuario.Email));
        }
        #endregion
    }
}
