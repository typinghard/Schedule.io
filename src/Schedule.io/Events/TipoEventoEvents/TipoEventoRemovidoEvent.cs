using Schedule.io.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Events.TipoEventoEvents
{
    public class TipoEventoRemovidoEvent : Event
    {
        public string Id { get; set; }

        public TipoEventoRemovidoEvent(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
