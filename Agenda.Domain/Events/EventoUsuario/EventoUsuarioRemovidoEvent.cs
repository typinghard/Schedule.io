using Agenda.Domain.Core.Messages;
using System;

namespace Agenda.Domain.Events
{
    public class EventoUsuarioRemovidoEvent : Event
    {
        public string Id { get; set; }

        public EventoUsuarioRemovidoEvent(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
