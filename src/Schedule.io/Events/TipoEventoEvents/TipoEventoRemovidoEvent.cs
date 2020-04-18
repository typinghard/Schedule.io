using Schedule.io.Core.Messages;

namespace Schedule.io.Events.TipoEventoEvents
{
    public class TipoEventoRemovidoEvent : Event
    {
        public string Id { get; set; }

        public TipoEventoRemovidoEvent(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
