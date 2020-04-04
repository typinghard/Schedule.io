using Schedule.io.Core.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Events.Usuario
{
   public class UsuarioAtualizadoEvent : Event
    {
        public string Id { get; set; }
        public string UsuarioEmail { get; set; }

        public UsuarioAtualizadoEvent(string id, string usuarioEmail)
        {
            this.Id = id;
            this.AggregateId = id;
            this.UsuarioEmail = usuarioEmail;
        }
    }
}
