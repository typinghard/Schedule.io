using Schedule.io.Core.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Events.AgendaEvents
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
