using Schedule.io.Core.Messages;

namespace Schedule.io.Events.AgendaEvents
{
    public class AgendaRemovidaEvent : Event
    {
        public string Id { get; private set; }
        public AgendaRemovidaEvent(string id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
