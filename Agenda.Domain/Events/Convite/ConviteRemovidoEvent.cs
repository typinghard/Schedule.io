using Agenda.Domain.Core.Messages;
using System;

namespace Agenda.Domain.Events
{
    public class ConviteRemovidoEvent : Event
    {
        public Guid Id { get; set; }

        public ConviteRemovidoEvent(Guid id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
