using Microsoft.EntityFrameworkCore;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public class AgendaUsuarioRepository : Repository<AgendaUsuario>, IAgendaUsuarioRepository
    {
        public AgendaUsuarioRepository(AgendaContext context) : base(context)
        {
        }
        public AgendaUsuario ObterPorAgendaIdEUsuarioId(string agendaId, string usuarioId)
        {
            return DbSet.AsNoTracking()
                .FirstOrDefault(x=>x.AgendaId == agendaId
                                && x.UsuarioId == usuarioId
                                && !x.Inativo);
        }
    }
}
