using Schedule.io.Core.Messages;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Events.AgendaUsuarioEvents
{
    public class AgendaUsuarioAtualizadoEvent : Event
    {
        public string Id { get; set; }
        public string AgendaId { get; set; }
        public string UsuarioId { get; set; }

        public PermissoesAgenda Permissoes { get; set; }

        public AgendaUsuarioAtualizadoEvent(string id, string agendaId, string usuarioId, PermissoesAgenda permissoes)
        {
            this.Id = id;
            this.AggregateId = id;
            this.AgendaId = agendaId;
            this.UsuarioId = usuarioId;
            this.Permissoes = permissoes;
        }
    }
}
