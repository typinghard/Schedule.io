using Schedule.io.Core.Messages;

namespace Schedule.io.Events.EventoAgendaEvents
{
    public class EventoRemovidoEvent : Event
    {
        public string Id { get; private set; }

        public EventoRemovidoEvent(string id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
