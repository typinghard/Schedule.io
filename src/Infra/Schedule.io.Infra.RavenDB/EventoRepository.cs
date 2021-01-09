using Raven.Client.Documents.Session;
using Schedule.io.Extensions;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.io.Infra.RavenDB
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public EventoRepository(IDocumentSession session) : base(session)
        { }

        public IList<Convite> ListarConvites(string eventoId)
        {
            return Sessao
                 .Query<Convite>()
                 .Where(x => x.EventoId == eventoId)
                 .ToList();
        }

        public IList<Evento> Listar(string agendaId)
        {
            return Sessao
                 .Query<Evento>()
                 .Where(x => x.AgendaId == agendaId)
                 .ToList();
        }

        public IList<Evento> Listar(string agendaId, DateTime dataInicio, DateTime dataFinal)
        {
            return Sessao
                .Query<Evento>()
                .Where(x => x.AgendaId == agendaId
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

            whereDinamico = whereDinamico.And(x => x.AgendaId == agendaId);

            return Sessao
                    .Query<Evento>()
                    .Where(whereDinamico)
                    .ToList();
        }

        public IList<Evento> Listar(string agendaId, string usuarioId)
        {
            return Sessao
                 .Query<Evento>()
                 .Where(x => x.AgendaId == agendaId
                        && x.UsuarioIdCriador == usuarioId)
                 .ToList();
        }
    }
}