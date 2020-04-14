using MongoDB.Driver;
using Schedule.io.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;

namespace Schedule.io.Infra.MongoDB
{
    public class EventoAgendaRepository : Repository<Evento>, IEventoAgendaRepository
    {
        public EventoAgendaRepository(AgendaContext context) : base(context)
        {

        }

        public void AdicionarConvite(Convite convite)
        {
            Db.Convite.InsertOne(Db.Session, convite);
            SalvarAlteracoes();
        }

        public void ExcluirConvite(Convite convite)
        {
            Db.Convite.DeleteOne(c => c.EventoId == convite.EventoId && c.EmailConvidado == convite.EmailConvidado);
            SalvarAlteracoes();
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
                      && (x.DataFinal == null || x.DataFinal <= dataFinal)
                     )
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
