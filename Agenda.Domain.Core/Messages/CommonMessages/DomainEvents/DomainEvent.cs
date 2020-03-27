using System;
using MediatR;

namespace Agenda.Domain.Core.Messages.CommonMessages.DomainEvents
{
    public abstract class DomainEvent : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent(string aggregateId)
        {
            AggregateId = aggregateId;
            Timestamp = DateTime.Now;
        }
    }
}
