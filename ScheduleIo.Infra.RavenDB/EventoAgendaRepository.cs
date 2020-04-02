﻿using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleIo.Infra.RavenDB
{
    public class EventoAgendaRepository : Repository<EventoAgenda>, IEventoAgendaRepository
    {
        public EventoAgendaRepository(IDocumentSession session) : base(session)
        {

        }

        public IList<EventoAgenda> ObterEventosDaAgenda(string agendaId)
        {
            return Sessao
                 .Query<EventoAgenda>()
                 .Where(x => x.AgendaId == agendaId)
                 .ToList();
        }

        public IList<EventoAgenda> ObterEventosPorPeriodo(string agendaId, DateTime dataInicio, DateTime dataFinal)
        {
            return Sessao
                .Query<EventoAgenda>()
                .Where(x => x.AgendaId == agendaId && x.DataInicio >= dataInicio && x.DataFinal <= dataFinal)
                .ToList();
        }

        public IList<EventoAgenda> ObterTodosEventosDoUsuario(string agendaId, string usuarioId)
        {
            return Sessao
                 .Query<EventoAgenda>()
                 .Where(x => x.AgendaId == agendaId && x.UsuarioId == usuarioId)
                 .ToList();
        }
    }
}
