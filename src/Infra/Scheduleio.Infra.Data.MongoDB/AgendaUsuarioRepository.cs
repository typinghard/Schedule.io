using MongoDB.Driver;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.Data.MongoDB
{
    public class AgendaUsuarioRepository : Repository<AgendaUsuario>, IAgendaUsuarioRepository
    {
        public AgendaUsuarioRepository(AgendaContext context) : base(context)
        {
        }
        public AgendaUsuario ObterPorAgendaIdEUsuarioId(string agendaId, string usuarioId)
        {
            return Db.AgendaUsuario.Find(x => x.AgendaId == agendaId
                                         && x.UsuarioId == usuarioId
                                         && !x.Inativo).FirstOrDefault();
        }
    }
}
