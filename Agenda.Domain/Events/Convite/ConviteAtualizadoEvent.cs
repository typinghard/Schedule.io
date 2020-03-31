using Agenda.Domain.Core.Messages;
using Agenda.Domain.Enums;
using Agenda.Domain.Models;
using System;


namespace Agenda.Domain.Events
{
    public class ConviteAtualizadoEvent : Event
    {
        public string Id { get; set; }
        public string EventoId { get; set; }
        public string UsuarioId { get; set; }
        public string EmailConvidado { get; set; }
        public EnumStatusConviteEvento Status { get; set; }
        public PermissoesConvite Permissoes { get; set; }

        public ConviteAtualizadoEvent(string id, string eventoId, string usuarioId, string emailConvidado, EnumStatusConviteEvento status, PermissoesConvite permissoes)
        {
            this.Id = id;
            this.EventoId = eventoId;
            this.UsuarioId = usuarioId;
            this.EmailConvidado = emailConvidado;
            this.AggregateId = id;
            this.Status = status;
            this.Permissoes = permissoes;
        }
    }
}
