using Schedule.io.Core.Messages;

namespace Schedule.io.Events.UsuarioEvents
{
    public class UsuarioAtualizadoEvent : Event
    {
        public string Id { get; private set; }
        public string UsuarioEmail { get; private set; }

        public UsuarioAtualizadoEvent(string id, string usuarioEmail)
        {
            Id = id;
            AggregateId = id;
            UsuarioEmail = usuarioEmail;
        }
    }
}
