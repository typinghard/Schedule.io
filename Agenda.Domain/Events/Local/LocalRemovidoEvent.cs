using Agenda.Domain.Core.Messages;
using System;


namespace Agenda.Domain.Events
{
    public class LocalRemovidoEvent : Event
    {
        public string Id { get; set; }

        public LocalRemovidoEvent(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
