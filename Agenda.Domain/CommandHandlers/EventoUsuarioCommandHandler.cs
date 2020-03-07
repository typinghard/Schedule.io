using Agenda.Domain.Commands;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;
using Agenda.Domain.Events;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Domain.CommandHandlers
{
    public class EventoUsuarioCommandHandler : CommandHandler,
        IRequestHandler<RegistrarEventoUsuarioCommand, bool>,
        IRequestHandler<AtualizarEventoUsuarioCommand, bool>,
        IRequestHandler<RemoverEventoUsuarioCommand, bool>
    {
        private readonly IEventoUsuarioRepository _eventoUsuarioRepository;
        private readonly IMediatorHandler Bus;

        public EventoUsuarioCommandHandler(IEventoUsuarioRepository eventoUsuarioRepository,
                                           IUnitOfWork uow,
                                           IMediatorHandler bus,
                                           INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            this._eventoUsuarioRepository = eventoUsuarioRepository;
            this.Bus = bus;
        }

        public Task<bool> Handle(RegistrarEventoUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            EventoUsuario eventoUsuario = new EventoUsuario(message.UsuarioId, message.Confirmacao, message.Permissao);
            _eventoUsuarioRepository.Adicionar(eventoUsuario);

            if (Commit())
            {
                Bus.PublicarEvento(new EventoUsuarioRegistradoEvent(eventoUsuario.Id, eventoUsuario.UsuarioId, eventoUsuario.Confirmacao, eventoUsuario.Permissao));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(AtualizarEventoUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            EventoUsuario eventoUsuario = _eventoUsuarioRepository.ObterPorId(message.Id);
            if (eventoUsuario == null)
            {
                Bus.PublicarNotificacao(new DomainNotification("eventoUsuario", "EventoUsuario não encontrado pelo Id!")).Wait();
                return Task.FromResult(false);
            }

            eventoUsuario.DefinirUsuarioId(message.UsuarioId);
            eventoUsuario.DefinirStatusConfirmacaoDoUsuario(message.Confirmacao);
            eventoUsuario.DefinirPermissao(message.Permissao);

            _eventoUsuarioRepository.Atualizar(eventoUsuario);
            if (Commit())
            {
                Bus.PublicarEvento(new EventoUsuarioAtualizadoEvent(eventoUsuario.Id, eventoUsuario.UsuarioId, eventoUsuario.Confirmacao, eventoUsuario.Permissao));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoverEventoUsuarioCommand message, CancellationToken cancellationToken)
        {
            EventoUsuario eventoUsuario = _eventoUsuarioRepository.ObterPorId(message.Id);
            _eventoUsuarioRepository.Remover(eventoUsuario);

            Bus.PublicarEvento(new EventoAgendaRemovidoEvent(eventoUsuario.Id)).Wait();
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _eventoUsuarioRepository.Dispose();
        }
    }
}
