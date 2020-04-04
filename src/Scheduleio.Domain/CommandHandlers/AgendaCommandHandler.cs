﻿using Schedule.io.Core.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Commands.AgendaCommands;
using Schedule.io.Core.Events.AgendaEvents;
using Schedule.io.Core.Core.Communication.Mediator;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Core.Models;

namespace Schedule.io.Core.CommandHandlers
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

            Agenda agenda = new Agenda(message.Id, message.Titulo);

            if (!string.IsNullOrEmpty(message.Descricao))
                agenda.DefinirDescricao(message.Descricao);

            if (message.Publico)
                agenda.TornarAgendaPublica();

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

            Models.Agenda agenda = _agendaRepository.ObterPorId(message.Id);
            if (agenda == null)
            {
                Bus.PublicarNotificacao(new DomainNotification("agenda", "Agenda não encontrada pelo Id!")).Wait();
                return Task.FromResult(false);
            }

            agenda.DefinirTitulo(message.Titulo);

            if (!string.IsNullOrEmpty(message.Descricao))
                agenda.DefinirDescricao(message.Descricao);

            if (message.Publico)
                agenda.TornarAgendaPublica();
            else
                agenda.TornarAgendaPrivado();

            _agendaRepository.Atualizar(agenda);
            if (Commit())
            {
                Bus.PublicarEvento(new AgendaAtualizadaEvent(agenda.Id, agenda.Titulo, agenda.Descricao, agenda.Publico));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoverAgendaCommand message, CancellationToken cancellationToken)
        {
            Agenda agenda = _agendaRepository.ObterPorId(message.Id);
            _agendaRepository.Remover(agenda);

            if (Commit())
            {
                Bus.PublicarEvento(new AgendaRemovidaEvent(agenda.Id)).Wait();
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _agendaRepository.Dispose();
        }
    }
}