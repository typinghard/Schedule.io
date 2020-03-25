using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Infra.MongoDB
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AgendaContext context) : base(context)
        {
        }
    }
}
