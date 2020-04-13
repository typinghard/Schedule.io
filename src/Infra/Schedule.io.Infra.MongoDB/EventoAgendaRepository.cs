using MongoDB.Driver;
using Schedule.io.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.MongoDB
{
    public class EventoAgendaRepository : Repository<Evento>, IEventoAgendaRepository
    {
        public EventoAgendaRepository(AgendaContext context) : base(context)
        {

        }

        public IList<Evento> ObterEventosDaAgenda(string agendaId)
        {
            return Db.EventoAgenda
                .Find(x => x.AgendaId == agendaId
                      && !x.Inativo)
                .ToList();
        }

        public IList<Evento> ObterEventosPorPeriodo(string agendaId, DateTime dataInicio, DateTime dataFinal)
        {
            return Db.EventoAgenda
                .Find(x => x.AgendaId == agendaId
                      && x.DataInicio >= dataInicio
                      && (x.DataFinal == null || x.DataFinal <= dataFinal)
                      && !x.Inativo)
                .ToList();
        }

        public IList<Evento> ObterTodosEventosDoUsuario(string agendaId, string usuarioId)
        {

            return Db.EventoAgenda
                .Find(x => x.AgendaId == agendaId
                      && x.UsuarioId == usuarioId
                      && !x.Inativo)
                .ToList();

        }
    }
}
