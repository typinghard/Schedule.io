using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Infra.Data
{
    public class EventoUsuarioRepository : Repository<EventoUsuario>, IEventoUsuarioRepository
    {
        public EventoUsuarioRepository(AgendaContext context) : base(context)
        {
        }
    }
}
