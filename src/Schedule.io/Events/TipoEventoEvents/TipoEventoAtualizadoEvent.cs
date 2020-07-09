using Schedule.io.Core.Messages;

namespace Schedule.io.Events.TipoEventoEvents
{
    public class TipoEventoAtualizadoEvent : Event
    {
        public string Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        public TipoEventoAtualizadoEvent(string id, string nome, string descricao)
        {
            Id = id;
            AggregateId = id;
            Nome = nome;
            Descricao = descricao;
        }
    }
}
