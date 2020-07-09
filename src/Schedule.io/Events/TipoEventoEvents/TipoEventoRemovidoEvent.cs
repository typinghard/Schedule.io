using Schedule.io.Core.Messages;

namespace Schedule.io.Events.TipoEventoEvents
{
    public class TipoEventoRemovidoEvent : Event
    {
        public string Id { get; private set; }

        public TipoEventoRemovidoEvent(string id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
