using Agenda.Domain.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Events
{
    public class AgendaAtualizadaEvent : Event
    {
        public Guid Id { get; set; }
        public string Titulo { get;  set; }

        public string Descricao { get;  set; }

        public bool Publico { get;  set; }

        public AgendaAtualizadaEvent(Guid id, string titulo, string descricao, bool publico)
        {
            this.Id = id;
            this.AggregateId = id;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Publico = publico;
        }
    }
}
