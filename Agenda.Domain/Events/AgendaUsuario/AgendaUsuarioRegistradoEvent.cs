using Agenda.Domain.Core.Messages;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Events
{
    public class AgendaUsuarioRegistradoEvent : Event
    {
        public string Id { get; set; }
        public string AgendaId { get; set; }
        public string UsuarioId { get; set; }
        public PermissoesAgenda Permissoes { get; set; }

        public AgendaUsuarioRegistradoEvent(string id, string agendaId, string usuarioId, PermissoesAgenda permissoes)
        {
            this.Id = id;
            this.AggregateId = id;
            this.AgendaId = agendaId;
            this.UsuarioId = usuarioId;
            this.Permissoes = permissoes;
        }
    }
}
