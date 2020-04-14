using Schedule.io.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Events.AgendaEvents
{
   public class AgendaRemovidaEvent : Event
    {
        public string Id { get; set; }
        public AgendaRemovidaEvent(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
