using Schedule.io.Core.Messages;

namespace Schedule.io.Events.AgendaUsuarioEvents
{
    public class AgendaUsuarioRemovidoEvent : Event
    {
        public string Id { get; set; }

        public AgendaUsuarioRemovidoEvent(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
