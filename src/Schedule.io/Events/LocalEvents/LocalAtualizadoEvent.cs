using Schedule.io.Core.Core.Messages;
using System;

namespace Schedule.io.Events.LocalEvents
{
    public class LocalAtualizadoEvent : Event
    {
        public string Id { get; set; }
        public string IdentificadorExterno { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Reserva { get; set; }
        public int LotacaoMaxima { get;  set; }


        public LocalAtualizadoEvent(string id, string identificadorExterno, string nomeLocal, string descricao, bool reservaLocal, int lotacaoMaxima)
        {
            this.Id = id;
            this.AggregateId = id;
            this.IdentificadorExterno = identificadorExterno;
            this.Nome = nomeLocal;
            this.Descricao = descricao;
            this.Reserva = reservaLocal;
            this.LotacaoMaxima = lotacaoMaxima;
        }
    }
}
