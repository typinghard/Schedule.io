using MongoDB.Driver;
using Schedule.io.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using Schedule.io.Infra.MongoDB.Configs;

namespace Schedule.io.Infra.MongoDB
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public EventoRepository(ScheduleioContext context) : base(context)
        {

        }

        public IList<Convite> ListarConvites(string eventoId)
        {
            return Db.Convite
                     .Find(x => x.EventoId == eventoId)
                     .ToList();
        }

        public IList<Evento> ListarEventosDaAgenda(string agendaId)
        {
            return Db.Evento
                .Find(x => x.AgendaId == agendaId)
                .ToList();
        }

        public IList<Evento> ListarEventosPorPeriodo(string agendaId, DateTime dataInicio, DateTime dataFinal)
        {
            return Db.Evento
                .Find(x => x.AgendaId == agendaId
                      && x.DataInicio >= dataInicio
                      && (x.DataFinal == null || x.DataFinal <= dataFinal))
                .ToList();
        }

        public IList<Evento> ListarTodosEventosDoUsuario(string agendaId, string usuarioId)
        {

            return Db.Evento
                .Find(x => x.AgendaId == agendaId
                      && x.UsuarioIdCriador == usuarioId)
                .ToList();

        }
    }
}
