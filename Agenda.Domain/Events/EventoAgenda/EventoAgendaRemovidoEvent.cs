using System;
using Agenda.Domain.Core.Messages;

namespace Agenda.Domain.Events
{
    public class EventoAgendaRemovidoEvent : Event
    {
        public Guid Id { get; set; }

        public EventoAgendaRemovidoEvent(Guid id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
