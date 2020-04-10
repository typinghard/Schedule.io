using Schedule.io.Core.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Events.UsuarioEvents
{
    public class UsuarioRemovidoEvent : Event
    {
        public string Id { get; set; }

        public UsuarioRemovidoEvent(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }
    }
}
