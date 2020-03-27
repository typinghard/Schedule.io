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

        public IList<EventoAgenda> ObterTodosEventosUsuario(Guid eventoId, Guid usuarioId)
        {
            //var agendaUsuario = Db.AgendaUsuario.Find(x => x.AgendaId == agendaId).ToList();

            var eventosAgenda = Db.EventoAgenda.Find(x => x.Id == eventoId).ToList();

            var eventosUsuario = eventosAgenda.Where(x => x.Convites.Any(i => i.UsuarioId == usuarioId)).ToList();

            //var teste = Db.Convite.Find(c => c.UsuarioId == usuarioId && c.EventoId == c.Id).ToList();

            var teste1 = Db.EventoAgenda.Find(x => x.Id == eventoId).ToList().Where(c => c.Convites.Any(i => i.UsuarioId == usuarioId)).ToList();

            //var teste2 = Db.EventoAgenda.Find(x=>x.Id == eventoId).ToList().SelectMany(i=> i.Convites.Where(c => c.UsuarioId == usuarioId).ToList());

            return teste1;

        }
    }
}
