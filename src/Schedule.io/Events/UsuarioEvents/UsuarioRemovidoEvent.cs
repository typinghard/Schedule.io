using Schedule.io.Core.Messages;

namespace Schedule.io.Events.UsuarioEvents
{
    public class UsuarioRemovidoEvent : Event
    {
        public string Id { get; private set; }

        public UsuarioRemovidoEvent(string id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
