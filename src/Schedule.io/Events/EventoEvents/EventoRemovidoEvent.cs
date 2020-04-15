using Schedule.io.Core.Messages;

namespace Schedule.io.Events.EventoAgendaEvents
{
    public class EventoRemovidoEvent : Event
    {
        public string Id { get; set; }

        public EventoRemovidoEvent(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
