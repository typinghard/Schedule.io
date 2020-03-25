using Agenda.Domain.Core.Messages;
using System;

namespace Agenda.Domain.Events
{
    public class LocalRegistradoEvent : Event
    {
        public string Id { get; set; }
        public string IdentificadorExterno { get; set; }
        public string NomeLocal { get; set; }
        public string Descricao { get; set; }
        public bool ReservaLocal { get; set; }
        public int LotacaoMaxima { get; set; }

        public LocalRegistradoEvent(string id, string identificadorExterno, string nomeLocal, string descricao, bool reservaLocal, int lotacaoMaxima)
        {
            this.Id = id;
            this.AggregateId = id;
            this.IdentificadorExterno = identificadorExterno;
            this.NomeLocal = nomeLocal;
            this.Descricao = descricao;
            this.ReservaLocal = reservaLocal;
            this.LotacaoMaxima = lotacaoMaxima;
        }
    }
}
