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
            if (string.IsNullOrEmpty(usuario.Id))
                RegistrarUsuario(usuario);
            else
                AtualizarUsuario(usuario);

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

        #region Privados
        private Usuario RegistrarUsuario(Usuario usuario)
        {
            var novoUsuario = new Usuario(Guid.NewGuid().ToString(), usuario.Email);
            novoUsuario.DefinirDataCriacao();
            novoUsuario.DefinirDataAtualizacao();

            _usuarioRepository.Adicionar(novoUsuario);

            if (Commit())
                _bus.PublicarEvento(new UsuarioRegistradoEvent(novoUsuario.Id, novoUsuario.Email));

            ValidarComando();

            return novoUsuario;
        }

        private Usuario AtualizarUsuario(Usuario usuario)
        {
            var atualizarUsuario = RecuperaUsuarioEValida(usuario.Id);

            atualizarUsuario.DefinirDataAtualizacao();
            atualizarUsuario.DefinirEmail(usuario.Email);

            _usuarioRepository.Atualizar(atualizarUsuario);

            if (Commit())
                _bus.PublicarEvento(new UsuarioAtualizadoEvent(atualizarUsuario.Id, atualizarUsuario.Email));

            ValidarComando();

            return atualizarUsuario;
        }

        private Usuario RecuperaUsuarioEValida(string usuarioId)
        {
            var usuario = _usuarioRepository.Obter(usuarioId);

            if (usuario == null)
                throw new ScheduleIoException(new List<string>() { "Usuario não encontrado!" });

            return usuario;
        } 
        #endregion
    }
}
