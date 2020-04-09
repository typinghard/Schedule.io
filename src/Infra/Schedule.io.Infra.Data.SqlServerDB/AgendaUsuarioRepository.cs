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
        public AgendaUsuario ObterAgendaDoUsuario(string agendaId, string usuarioId)
        {
            return Obter(@$"
                             SELECT {_atributosBase}, 
                                    AgendaId, UsuarioId 
                             FROM {_table}  
                             WHERE AgendaId = '{agendaId}'
                             and UsuarioId = '{usuarioId}'
                             and {_inativoFalse}
            ");
        }
    }
}
