using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Infra.MongoDB
{
    public class AgendaUsuarioRepository : Repository<AgendaUsuario>, IAgendaUsuarioRepository
    {
        public AgendaUsuarioRepository(AgendaContext context) : base(context)
        {
        }

        public AgendaUsuario ObterPorAgendaIdEUsuarioId(string agendaId, string usuarioId)
        {
            return Db.AgendaUsuario.Find(x => x.AgendaId == agendaId && x.UsuarioId == usuarioId).FirstOrDefault();
        }
    }
}
