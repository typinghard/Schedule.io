using Raven.Client.Documents.Session;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.RavenDB
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
                 .Where(x => x.AgendaId == agendaId 
                        && !x.Inativo)
                 .ToList();
        }

        public IList<EventoAgenda> ObterEventosPorPeriodo(string agendaId, DateTime dataInicio, DateTime dataFinal)
        {
            return Sessao
                .Query<EventoAgenda>()
                .Where(x => x.AgendaId == agendaId 
                       && x.DataInicio >= dataInicio
                       && (x.DataFinal == null || x.DataFinal <= dataFinal)
                       && !x.Inativo)
                .ToList();
        }

        public IList<EventoAgenda> ObterTodosEventosDoUsuario(string agendaId, string usuarioId)
        {
            return Sessao
                 .Query<EventoAgenda>()
                 .Where(x => x.AgendaId == agendaId 
                        && x.UsuarioId == usuarioId
                        && !x.Inativo)
                 .ToList();
        }
    }
}
