using Agenda.Domain.Core.Messages;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Events
{
    public class AgendaUsuarioAtualizadoEvent : Event
    {
        public Guid Id { get; set; }
        public Guid AgendaId { get; set; }
        public Guid UsuarioId { get; set; }

        public PermissoesAgenda Permissoes { get; set; }

        public AgendaUsuarioAtualizadoEvent(Guid id, Guid agendaId, Guid usuarioId, PermissoesAgenda permissoes)
        {
            this.Id = id;
            this.AggregateId = id;
            this.AgendaId = agendaId;
            this.UsuarioId = usuarioId;
            this.Permissoes = permissoes;
        }
    }
}
