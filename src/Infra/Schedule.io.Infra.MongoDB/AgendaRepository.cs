using MongoDB.Driver;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.MongoDB
{
    public class AgendaRepository : Repository<Agenda>, IAgendaRepository
    {
        public AgendaRepository(AgendaContext context) : base(context)
        {
        }

        public Agenda ObterAgendaPorUsuarioId(string agendaId, string usuarioId)
        {
            var agendaUsuario = Db.AgendaUsuario.Find(c => c.AgendaId == agendaId && c.UsuarioId == usuarioId).FirstOrDefault();

            if (agendaUsuario == null)
                return null;

            return Db.Agenda.Find(x => x.Id == agendaUsuario.AgendaId).FirstOrDefault();
        }
    }
}
