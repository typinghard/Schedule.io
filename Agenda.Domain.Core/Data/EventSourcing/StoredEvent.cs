using System;

namespace Agenda.Core.Data.EventSourcing
{
    public class StoredEvent
    {
        public StoredEvent(string id, string aggregatedId, string tipo, DateTime dataOcorrencia, string dados)
        {
            Id = id;
            AggregatedId = aggregatedId;
            Tipo = tipo;
            DataOcorrencia = dataOcorrencia;
            Dados = dados;
        }

        public string Id { get; private set; }

        public string AggregatedId { get; private set; }

        public string Tipo { get; private set; }

        public DateTime DataOcorrencia { get; set; }

        public string Dados { get; private set; }
    }
}
