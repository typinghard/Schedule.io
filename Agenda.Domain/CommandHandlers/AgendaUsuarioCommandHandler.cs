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
    public class AgendaUsuarioCommandHandler : CommandHandler,
        IRequestHandler<RegistrarAgendaUsuarioCommand, bool>,
        IRequestHandler<AtualizarAgendaUsuarioCommand, bool>,
        IRequestHandler<RemoverAgendaUsuarioCommand, bool>
    {
        private readonly IAgendaUsuarioRepository _agendaUsuarioRepository;
        private readonly IMediatorHandler Bus;

        public AgendaUsuarioCommandHandler(IAgendaUsuarioRepository agendaUsuarioRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            this._agendaUsuarioRepository = agendaUsuarioRepository;
            this.Bus = bus;
        }

        public Task<bool> Handle(RegistrarAgendaUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            AgendaUsuario agendaUsuario = new AgendaUsuario(message.Id, message.AgendaId, message.UsuarioId);
            _agendaUsuarioRepository.Adicionar(agendaUsuario);

            if (Commit())
            {
                Bus.PublicarEvento(new AgendaUsuarioRegistradoEvent(agendaUsuario.Id, agendaUsuario.AgendaId, agendaUsuario.UsuarioId, agendaUsuario.Permissoes));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(AtualizarAgendaUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            AgendaUsuario agendaUsuario = _agendaUsuarioRepository.ObterPorId(message.Id);
            if (agendaUsuario == null)
            {
                Bus.PublicarNotificacao(new DomainNotification("agendaUsuario", "Agenda do usuário não encontrada pelo Id!")).Wait();
                return Task.FromResult(false);
            }

            _agendaUsuarioRepository.Remover(agendaUsuario);
            Bus.PublicarEvento(new AgendaUsuarioRemovidoEvent(agendaUsuario.Id)).Wait();

            AgendaUsuario novoAgendaUsuario = new AgendaUsuario(message.Id, message.AgendaId, message.UsuarioId);
            _agendaUsuarioRepository.Adicionar(novoAgendaUsuario);
            if (Commit())
            {
                Bus.PublicarEvento(new AgendaUsuarioRegistradoEvent(agendaUsuario.Id, agendaUsuario.AgendaId, agendaUsuario.UsuarioId, agendaUsuario.Permissoes));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoverAgendaUsuarioCommand message, CancellationToken cancellationToken)
        {
            AgendaUsuario agendaUsuario = _agendaUsuarioRepository.ObterPorId(message.Id);
            _agendaUsuarioRepository.Remover(agendaUsuario);

            Bus.PublicarEvento(new AgendaUsuarioRemovidoEvent(agendaUsuario.Id)).Wait();
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _agendaUsuarioRepository.Dispose();
        }
    }
}
