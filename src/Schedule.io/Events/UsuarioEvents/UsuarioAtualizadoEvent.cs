using Schedule.io.Core.Messages;

namespace Schedule.io.Events.UsuarioEvents
{
    public class UsuarioAtualizadoEvent : Event
    {
        public string Id { get; set; }
        public string UsuarioEmail { get; set; }

        public UsuarioAtualizadoEvent(string id, string usuarioEmail)
        {
            this.Id = id;
            this.AggregateId = id;
            this.UsuarioEmail = usuarioEmail;
        }
    }
}
