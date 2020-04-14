using Schedule.io.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Events.TipoEventoEvents
{
    public class TipoEventoRegistradoEvent : Event
    {
        public string Id { get; set; }
        public string  Nome { get; set; }
        public string Descricao { get; set; }

        public TipoEventoRegistradoEvent(string id, string nome, string descricao)
        {
            this.Id = id;
            this.AggregateId = id;
            this.Nome = nome;
            this.Descricao = descricao;
        }
    }
}
