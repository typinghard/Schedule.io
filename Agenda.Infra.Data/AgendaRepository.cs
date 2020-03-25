using Agenda.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Infra.MongoDB
{
    public class AgendaRepository : Repository<Agenda.Domain.Models.Agenda>, IAgendaRepository
    {
        public AgendaRepository(AgendaContext context) : base(context)
        {
        }
    }
}
