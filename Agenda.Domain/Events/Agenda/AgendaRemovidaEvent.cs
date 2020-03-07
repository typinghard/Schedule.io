using Agenda.Domain.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Events
{
   public class AgendaRemovidaEvent : Event
    {
        public Guid Id { get; set; }
        public AgendaRemovidaEvent(Guid id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
