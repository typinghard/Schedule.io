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

        public string Gravar(Usuario usuario)
        {
            Usuario gravarUsuario;
            if (string.IsNullOrEmpty(usuario.Id))
            {
                gravarUsuario = new Usuario(Guid.NewGuid().ToString(), usuario.Email);
                gravarUsuario.DefinirDataCriacao();
            }
            else
                gravarUsuario = new Usuario(usuario.Id, usuario.Email);

            gravarUsuario.DefinirDataAtualizacao();

            if (string.IsNullOrEmpty(usuario.Id))
                _usuarioRepository.Adicionar(usuario);
            else
                _usuarioRepository.Atualizar(usuario);

            if (Commit())
                if (string.IsNullOrEmpty(usuario.Id))
                    _bus.PublicarEvento(new UsuarioRegistradoEvent(usuario.Id, usuario.Email)).Wait();
                else
                    _bus.PublicarEvento(new UsuarioAtualizadoEvent(usuario.Id, usuario.Email)).Wait();

            ValidarComando();

            return usuario.Id;
        }



        public Usuario Obter(string usuarioId)
        {
            var usuario = RecuperaUsuarioEValida(usuarioId);

            ValidarComando();

            return usuario;
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
            var usuario = RecuperaUsuarioEValida(usuarioId);

            _usuarioRepository.Excluir(usuario);

            if (Commit())
                _bus.PublicarEvento(new UsuarioRemovidoEvent(usuario.Id)).Wait();

            ValidarComando();
        }

        private Usuario RecuperaUsuarioEValida(string usuarioId)
        {
            var usuario = _usuarioRepository.Obter(usuarioId);

            if (usuario == null)
                throw new ScheduleIoException(new List<string>() { "Usuario não encontrado!" });

            return usuario;
        }
    }
}
