using MongoDB.Driver;
using Schedule.io.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using Schedule.io.Infra.MongoDB.Configs;
using Schedule.io.Extensions;

namespace Schedule.io.Infra.MongoDB
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public EventoRepository(ScheduleioContext context) : base(context)
        { }

        public IList<Evento> Listar(string agendaId, string usuarioId)
        {
            return Db.Evento
                .Find(x => x.AgendaId == agendaId
                      && x.UsuarioIdCriador == usuarioId)
                .ToList();
        }

        public IList<Evento> Listar(string agendaId)
        {
            return Db.Evento
                     .Find(x => x.AgendaId == agendaId)
                     .ToList();
        }

        public IList<Evento> Listar(string agendaId, DateTime dataInicio, DateTime dataFinal)
        {
            return Db.Evento
                     .Find(x => x.AgendaId == agendaId
                           && x.DataInicio >= dataInicio
                           && (x.DataFinal == null || x.DataFinal <= dataFinal))
                     .ToList();
        }

        public IList<Evento> Listar(string agendaId, List<DateTime> datasInicio, List<DateTime?> datasFinal)
        {
            var whereDinamico = PredicadoExtensions.Iniciar<Evento>();
            foreach (var dataInicio in datasInicio)
                whereDinamico = whereDinamico.And(x => x.DataInicio.Ticks >= dataInicio.Ticks);

            if (datasFinal.Any())
                foreach (var dataFinal in datasFinal)
                    whereDinamico = whereDinamico.And(x => x.DataFinal == null || x.DataFinal.Value.Ticks <= dataFinal.Value.Ticks);

            return Db.Evento
                    .Find(whereDinamico)
                    .ToList();
        }

        public IList<Convite> ListarConvites(string eventoId)
        {
            return Db.Convite
                     .Find(x => x.EventoId == eventoId)
                     .ToList();
        }
    }
}