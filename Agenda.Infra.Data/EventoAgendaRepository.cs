using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleIo.Infra.MongoDB
{
    public class EventoAgendaRepository : Repository<EventoAgenda>, IEventoAgendaRepository
    {
        public EventoAgendaRepository(AgendaContext context) : base(context)
        {

        }
    }
}
