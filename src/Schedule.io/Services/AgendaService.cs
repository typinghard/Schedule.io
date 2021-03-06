﻿using MediatR;
using Schedule.io.Core.Communication.Mediator;
using Schedule.io.Core.Helpers;
using Schedule.io.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Events.AgendaEvents;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Interfaces.Services;
using Schedule.io.Models.AggregatesRoots;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.io.Services
{
    internal class AgendaService : ServiceBase, IAgendaService
    {
        private readonly IAgendaRepository _agendaRepository;

        public AgendaService(
            IAgendaRepository agendaRepository,
            IMediatorHandler bus,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications) : base(bus, uow, notifications)
        {
            _agendaRepository = agendaRepository;
        }

        public void Gravar(Agenda agenda)
        {
            var agendaQuery = _agendaRepository.Obter(agenda.Id);
            if (agendaQuery == null)
                Registrar(agenda);
            else
                Atualizar(agenda);

            ValidarComando();
        }

        public Agenda Obter(string agendaId)
        {
            return _agendaRepository.Obter(agendaId);
        }

        public IEnumerable<Agenda> Listar()
        {
            var agendas = _agendaRepository.Listar();
            foreach (var agenda in agendas)
            {
                yield return agenda;
            }
        }

        public IEnumerable<Agenda> Listar(string usuarioId)
        {
            var agendas = _agendaRepository.Listar(usuarioId);
            foreach (var agenda in agendas)
            {
                yield return agenda;
            }
        }

        public void Excluir(string agendaId)
        {
            var agenda = _agendaRepository.Obter(agendaId);
            if (agenda == null)
            {
                _bus.PublicarNotificacao(new DomainNotification("", "Agenda não encontrada!"));
                ValidarComando();
            }

            _agendaRepository.Excluir(agenda);

            if (Commit())
                _bus.PublicarEvento(new AgendaRemovidaEvent(agenda.Id)).Wait();

            ValidarComando();
        }

        #region Privados
        private void Registrar(Agenda agenda)
        {
            _agendaRepository.Adicionar(agenda);

            if (Commit())
                _bus.PublicarEvento(new AgendaRegistradaEvent(agenda.Id, agenda.UsuarioIdCriador, agenda.Titulo, agenda.Descricao, agenda.Publico));
        }

        private void Atualizar(Agenda agenda)
        {
            _agendaRepository.Atualizar(agenda);

            if (Commit())
                _bus.PublicarEvento(new AgendaAtualizadaEvent(agenda.Id, agenda.UsuarioIdCriador, agenda.Titulo, agenda.Descricao, agenda.Publico));
        }
        #endregion
    }
}
