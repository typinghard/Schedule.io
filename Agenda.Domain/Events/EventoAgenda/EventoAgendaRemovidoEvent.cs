using System;
using Agenda.Domain.Core.Messages;

namespace Agenda.Domain.Events
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
