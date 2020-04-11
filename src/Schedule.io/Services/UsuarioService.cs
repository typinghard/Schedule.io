using MediatR;
using Schedule.io.Core.Commands.UsuarioCommands;
using Schedule.io.Core.Core.Communication.Mediator;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Core.Interfaces;
using Schedule.io.Interfaces;
using Schedule.io.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Services
{
    internal class UsuarioService : ServiceBase, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository,
                              IMediatorHandler bus,
                              INotificationHandler<DomainNotification> notifications) : base(bus, notifications)
        {
            _usuarioRepository = usuarioRepository;
        }

        public string Gravar(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Id) || Guid.Parse(usuario.Id) == Guid.Empty)
            {
                usuario.Id = Guid.NewGuid().ToString();
                _bus.EnviarComando(new RegistrarUsuarioCommand(usuario.Id, usuario.Email)).Wait();
            }
            else
                _bus.EnviarComando(new AtualizarUsuarioCommand(usuario.Id, usuario.Email)).Wait();

            ValidarComando();

            return usuario.Id;
        }

        public bool Excluir(string usuarioId)
        {
            _bus.EnviarComando(new RemoverUsuarioCommand(usuarioId)).Wait();
            ValidarComando();
            return true;
        }

        public Usuario Obter(string usuarioId)
        {
            var usuario = _usuarioRepository.ObterPorId(usuarioId);

            if (usuario == null)
                throw new ScheduleIoException(new List<string>() { "Usuario não encontrado!" });

            return new Usuario()
            {
                Id = usuario.Id,
                CriadoAs = usuario.CriadoAs,
                AtualizadoAs = usuario.AtualizadoAs,
                Email = usuario.Email
            };
        }

    }
}
