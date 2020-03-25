using Agenda.Domain.Commands;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;
using Agenda.Domain.Events;
using Agenda.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Domain.CommandHandlers
{
    public class AgendaCommandHandler : CommandHandler,
         IRequestHandler<RegistrarAgendaCommand, bool>,
         IRequestHandler<AtualizarAgendaCommand, bool>,
         IRequestHandler<RemoverAgendaCommand, bool>
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMediatorHandler Bus;

        public AgendaCommandHandler(IAgendaRepository agendaRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            this._agendaRepository = agendaRepository;
            this.Bus = bus;
        }

        public Task<bool> Handle(RegistrarAgendaCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            Models.Agenda agenda = new Agenda.Domain.Models.Agenda(message.Titulo, message.Descricao, message.Publico);
            _agendaRepository.Adicionar(agenda);

            if (Commit())
            {
                Bus.PublicarEvento(new AgendaRegistradaEvent(agenda.Id, agenda.Titulo, agenda.Descricao, agenda.Publico));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(AtualizarAgendaCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            Agenda.Domain.Models.Agenda agenda = _agendaRepository.ObterPorId(message.Id);
            if (agenda == null)
            {
                Bus.PublicarNotificacao(new DomainNotification("agenda", "Agenda não encontrada pelo Id!")).Wait();
                return Task.FromResult(false);
            }

            agenda.DefinirTitulo(message.Titulo);
            agenda.DefinirDescricao(message.Descricao);

            _agendaRepository.Atualizar(agenda);
            if (Commit())
            {
                Bus.PublicarEvento(new AgendaAtualizadaEvent(agenda.Id, agenda.Titulo, agenda.Descricao, agenda.Publico));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoverAgendaCommand message, CancellationToken cancellationToken)
        {
            Agenda.Domain.Models.Agenda agenda = _agendaRepository.ObterPorId(message.Id);
            _agendaRepository.Remover(agenda);

            Bus.PublicarEvento(new AgendaRemovidaEvent(agenda.Id)).Wait();
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _agendaRepository.Dispose();
        }
    }
}
