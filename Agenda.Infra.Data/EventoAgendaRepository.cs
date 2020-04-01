using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleIo.Infra.MongoDB
{
    public class EventoAgendaRepository : Repository<EventoAgenda>, IEventoAgendaRepository
    {
        public EventoAgendaRepository(AgendaContext context) : base(context)
        {

        }

        public IList<EventoAgenda> ObterTodosEventosDaAgenda(string agendaId)
        {
            return Db.EventoAgenda
                .Find(x => x.AgendaId == agendaId)
                .ToList();
        }

        public IList<EventoAgenda> ObterTodosEventosDoUsuario(string eventoId, string usuarioId)
        {

            return Db.EventoAgenda
                .Find(x => x.Id == eventoId && x.UsuarioId == usuarioId)
                .ToList();

        }
    }
}
