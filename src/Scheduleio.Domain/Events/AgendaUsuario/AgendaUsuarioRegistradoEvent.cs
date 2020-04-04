using Schedule.io.Core.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Events.AgendaUsuario
{
    public class AgendaUsuarioRegistradoEvent : Event
    {
        public string Id { get; set; }
        public string AgendaId { get; set; }
        public string UsuarioId { get; set; }

        public AgendaUsuarioRegistradoEvent(string id, string agendaId, string usuarioId)
        {
            this.Id = id;
            this.AggregateId = id;
            this.AgendaId = agendaId;
            this.UsuarioId = usuarioId;
            //this.Permissoes = permissoes;
        }
    }
}
