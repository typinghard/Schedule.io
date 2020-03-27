using Agenda.Domain.Core.Messages;
using System;

namespace Agenda.Domain.Events
{
    public class ConviteRemovidoEvent : Event
    {
        public string Id { get; set; }

        public ConviteRemovidoEvent(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
