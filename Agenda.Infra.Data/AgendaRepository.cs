using Agenda.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleIo.Infra.MongoDB
{
    public class AgendaRepository : Repository<Agenda.Domain.Models.Agenda>, IAgendaRepository
    {
        public AgendaRepository(AgendaContext context) : base(context)
        {
        }

        public Agenda.Domain.Models.Agenda ObterAgendaPorUsuarioId(string agendaId, string usuarioId)
        {
            var agendaUsuario = Db.AgendaUsuario.Find(c => c.AgendaId == agendaId && c.UsuarioId == usuarioId).FirstOrDefault();
            return Db.Agenda.Find(x => x.Id == agendaUsuario.AgendaId).FirstOrDefault();
        }
    }
}
