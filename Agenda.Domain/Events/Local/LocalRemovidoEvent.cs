using Agenda.Domain.Core.Messages;
using System;


namespace Agenda.Domain.Events
{
    public class LocalRemovidoEvent : Event
    {
        public Guid Id { get; set; }

        public LocalRemovidoEvent(Guid id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
