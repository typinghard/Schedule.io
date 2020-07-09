using Schedule.io.Core.Messages;

namespace Schedule.io.Events.AgendaEvents
{
    public class AgendaAtualizadaEvent : Event
    {
        public string Id { get; private set; }
        public string UsuarioIdCriador { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public bool Publico { get; private set; }


        public AgendaAtualizadaEvent(string id, string usuarioIdCriador, string titulo, string descricao, bool publico)
        {
            Id = id;
            AggregateId = id;
            Titulo = titulo;
            Descricao = descricao;
            Publico = publico;
            UsuarioIdCriador = usuarioIdCriador;
        }
    }
}
