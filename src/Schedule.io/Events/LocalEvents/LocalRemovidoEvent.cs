using Schedule.io.Core.Messages;

namespace Schedule.io.Events.LocalEvents
{
    public class LocalRemovidoEvent : Event
    {
        public string Id { get; private set; }

        public LocalRemovidoEvent(string id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
