using Schedule.io.Core.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Events.AgendaUsuario
{
    public class AgendaUsuarioRemovidoEvent : Event
    {
        public string Id { get; set; }

        public AgendaUsuarioRemovidoEvent(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
