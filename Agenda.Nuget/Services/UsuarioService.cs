using Agenda.Domain.Commands;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;
using Agenda.Domain.Interfaces;
using MediatR;
using ScheduleIo.Nuget.Interfaces;
using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Services
{
    internal class UsuarioService : ServiceBase, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMediatorHandler _bus;

        public UsuarioService(IUsuarioRepository usuarioRepository,
                              IMediatorHandler bus,
                              INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _usuarioRepository = usuarioRepository;
            _bus = bus;
        }

        public string Gravar(Usuario usuario)
        {
            string usuarioId;

            if (usuario.Id == Guid.NewGuid())
            {
                usuarioId = Guid.NewGuid().ToString();
                _bus.EnviarComando(new RegistrarUsuarioCommand(usuarioId, usuario.Email)).Wait();
            }
            else
            {
                usuarioId = usuario.Id;
                _bus.EnviarComando(new AtualizarUsuarioCommand(usuario.Id, usuario.Email)).Wait();
            }
            ValidarComando();
            return usuarioId;
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
            {
                throw new ScheduleIoException(new List<string>() { "Usuario não encontrado!" });
            }

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
