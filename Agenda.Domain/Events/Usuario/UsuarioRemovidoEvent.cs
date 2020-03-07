using Agenda.Domain.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Events
{
    public class UsuarioRemovidoEvent : Event
    {
        public Guid Id { get; set; }

        public UsuarioRemovidoEvent(Guid id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
