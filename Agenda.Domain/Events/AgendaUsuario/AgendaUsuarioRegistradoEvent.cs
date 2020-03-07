﻿using Agenda.Domain.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Events
{
    public class AgendaUsuarioRegistradoEvent : Event
    {
        public Guid Id { get; set; }
        public Guid AgendaId { get; set; }
        public Guid UsuarioId { get; set; }

        public AgendaUsuarioRegistradoEvent(Guid id, Guid agendaId, Guid usuarioId)
        {
            this.Id = id;
            this.AggregateId = id;
            this.AgendaId = agendaId;
            this.UsuarioId = usuarioId;
        }
    }
}
