using Schedule.io.Core.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Schedule.io.Core.Commands.UsuarioCommands;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Core.Communication.Mediator;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Core.Models;
using Schedule.io.Core.Events.UsuarioEvents;

namespace Schedule.io.Core.CommandHandlers
{
    public class UsuarioCommandHandler : CommandHandler,
        IRequestHandler<RegistrarUsuarioCommand, bool>,
        IRequestHandler<AtualizarUsuarioCommand, bool>,
        IRequestHandler<RemoverUsuarioCommand, bool>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMediatorHandler Bus;

        public UsuarioCommandHandler(IUsuarioRepository usuarioRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            this._usuarioRepository = usuarioRepository;
            this.Bus = bus;
        }

        public Task<bool> Handle(RegistrarUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            Usuario usuario = new Usuario(message.Id, message.UsuarioEmail);
            _usuarioRepository.Adicionar(usuario);

            if (Commit())
            {
                Bus.PublicarEvento(new UsuarioRegistradoEvent(usuario.Id, usuario.Email));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(AtualizarUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            Usuario usuario = _usuarioRepository.ObterPorId(message.Id);
            if (usuario == null)
            {
                Bus.PublicarNotificacao(new DomainNotification("usuario", "Usuario não encontrado pelo Id!")).Wait();
                return Task.FromResult(false);
            }

            usuario.DefinirEmail(message.UsuarioEmail);

            _usuarioRepository.Atualizar(usuario);
            if (Commit())
            {
                Bus.PublicarEvento(new UsuarioAtualizadoEvent(usuario.Id, usuario.Email));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoverUsuarioCommand message, CancellationToken cancellationToken)
        {
            Usuario usuario = _usuarioRepository.ObterPorId(message.Id);
            _usuarioRepository.Remover(usuario);

            Bus.PublicarEvento(new UsuarioRemovidoEvent(usuario.Id)).Wait();
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
        }
    }
}
