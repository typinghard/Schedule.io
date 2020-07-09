using Schedule.io.Core.Messages;

namespace Schedule.io.Events.TipoEventoEvents
{
    public class TipoEventoRegistradoEvent : Event
    {
        public string Id { get; private set; }
        public string  Nome { get; private set; }
        public string Descricao { get; private set; }

        public TipoEventoRegistradoEvent(string id, string nome, string descricao)
        {
            Id = id;
            AggregateId = id;
            Nome = nome;
            Descricao = descricao;
        }
    }
}
