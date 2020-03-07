using Agenda.Domain.Core.Messages;
using System;

namespace Agenda.Domain.Events
{
    public class EventoUsuarioRemovidoEvent : Event
    {
        public Guid Id { get; set; }

        public EventoUsuarioRemovidoEvent(Guid id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
