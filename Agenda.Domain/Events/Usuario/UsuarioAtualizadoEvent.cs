﻿using Agenda.Domain.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Events
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
