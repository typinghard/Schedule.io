﻿using Schedule.io.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Events.UsuarioEvents
{
    public class UsuarioRegistradoEvent : Event
    {
        public string Id { get; set; }
        public string UsuarioEmail { get; set; }

        public UsuarioRegistradoEvent(string id, string usuarioEmail)
        {
            this.Id = id;
            this.AggregateId = id;
            this.UsuarioEmail = usuarioEmail;
        }
    }
}
