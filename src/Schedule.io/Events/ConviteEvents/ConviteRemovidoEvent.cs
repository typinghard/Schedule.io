using Schedule.io.Core.Messages;
using System;

namespace Schedule.io.Events.ConviteEvents
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
