using Raven.Client.Documents.Session;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Interfaces.Services;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.RavenDB
{
    public class EventoAgendaRepository : Repository<Evento>, IEventoAgendaRepository
    {
        public EventoAgendaRepository(IDocumentSession session) : base(session)
        {

        }

        public IList<Evento> ObterEventosDaAgenda(string agendaId)
        {
            return Sessao
                 .Query<Evento>()
                 .Where(x => x.AgendaId == agendaId)
                 .ToList();
        }

        public IList<Evento> ObterEventosPorPeriodo(string agendaId, DateTime dataInicio, DateTime dataFinal)
        {
            return Sessao
                .Query<Evento>()
                .Where(x => x.AgendaId == agendaId 
                       && x.DataInicio >= dataInicio
                       && (x.DataFinal == null || x.DataFinal <= dataFinal))
                .ToList();
        }

        public IList<Evento> ObterTodosEventosDoUsuario(string agendaId, string usuarioId)
        {
            return Sessao
                 .Query<Evento>()
                 .Where(x => x.AgendaId == agendaId 
                        && x.UsuarioIdCriador == usuarioId)
                 .ToList();
        }
    }
}
