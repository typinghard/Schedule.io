using Agenda.Domain.Core.Messages;
using Agenda.Domain.Enums;
using Agenda.Domain.Models;
using System;

namespace Agenda.Domain.Events
{
    public class ConviteRegistradoEvent : Event
    {
        public Guid Id { get; set; }
        public Guid EventoId { get; set; }
        public Guid UsuarioId { get; set; }
        public EnumStatusConviteEvento Status { get; set; }
        public PermissoesConvite Permissoes { get; set; }

        public ConviteRegistradoEvent(Guid id, Guid eventoId, Guid usuarioId, EnumStatusConviteEvento status, PermissoesConvite permissoes)
        {
            this.Id = id;
            this.AggregateId = id;
            this.EventoId = eventoId;
            this.UsuarioId = usuarioId;
            this.Status = status;
            this.Permissoes = permissoes;
        }
    }
}
