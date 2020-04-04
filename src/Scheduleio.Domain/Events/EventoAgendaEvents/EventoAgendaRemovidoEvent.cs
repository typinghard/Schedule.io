using Schedule.io.Core.Core.Messages;
using System;

namespace Schedule.io.Core.Events.EventoAgendaEvents
{
    public class EventoAgendaRemovidoEvent : Event
    {
        public string Id { get; set; }

        public EventoAgendaRemovidoEvent(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
