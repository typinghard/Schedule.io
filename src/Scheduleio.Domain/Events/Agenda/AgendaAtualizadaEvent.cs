using Schedule.io.Core.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Events.Agenda
{
    public class AgendaAtualizadaEvent : Event
    {
        public string Id { get; set; }
        public string Titulo { get;  set; }
        public string Descricao { get;  set; }
        public bool Publico { get; set; }


        public AgendaAtualizadaEvent(string id, string titulo, string descricao, bool publico)
        {
            this.Id = id;
            this.AggregateId = id;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Publico = publico;
        }
    }
}
