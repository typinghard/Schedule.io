using Schedule.io.Core.Core.Messages;
using System;

namespace Schedule.io.Core.Events.Convite
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
