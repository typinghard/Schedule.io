using MongoDB.Driver;
using Schedule.io.Interfaces.Services;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.MongoDB
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
