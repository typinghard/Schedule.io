using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Infra.MongoDB
{
    public class EventoUsuarioRepository : Repository<EventoUsuario>, IEventoUsuarioRepository
    {
        public EventoUsuarioRepository(AgendaContext context) : base(context)
        {
        }
    }
}
