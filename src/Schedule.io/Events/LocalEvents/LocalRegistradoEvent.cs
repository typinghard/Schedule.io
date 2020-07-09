using Schedule.io.Core.Messages;

namespace Schedule.io.Events.LocalEvents
{
    public class LocalRegistradoEvent : Event
    {
        public string Id { get; private set; }
        public string IdentificadorExterno { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Reserva { get; private set; }
        public int LotacaoMaxima { get; private set; }

        public LocalRegistradoEvent(string id, string identificadorExterno, string nomeLocal, string descricao, bool reservaLocal, int lotacaoMaxima)
        {
            Id = id;
            AggregateId = id;
            IdentificadorExterno = identificadorExterno;
            Nome = nomeLocal;
            Descricao = descricao;
            Reserva = reservaLocal;
            LotacaoMaxima = lotacaoMaxima;
        }
    }
}
