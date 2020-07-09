using Schedule.io.Core.Messages;

namespace Schedule.io.Events.UsuarioEvents
{
    public class UsuarioRegistradoEvent : Event
    {
        public string Id { get; private set; }
        public string UsuarioEmail { get; private set; }

        public UsuarioRegistradoEvent(string id, string usuarioEmail)
        {
            Id = id;
            AggregateId = id;
            UsuarioEmail = usuarioEmail;
        }
    }
}
