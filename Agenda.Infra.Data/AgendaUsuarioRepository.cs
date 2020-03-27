using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
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
    }
}
