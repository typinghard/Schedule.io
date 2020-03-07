using Agenda.Domain.Core.Messages;
using Agenda.Domain.Models;
using System;


namespace Agenda.Domain.Events
{
    public class EventoUsuarioAtualizadoEvent : Event
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public bool Confirmacao { get; set; }
        public Permissao Permissao { get; set; }

        public EventoUsuarioAtualizadoEvent(Guid id, Guid usuarioId, bool confirmacao, Permissao permissao)
        {
            this.Id = id;
            this.UsuarioId = usuarioId;
            this.AggregateId = id;
            this.Confirmacao = confirmacao;
            this.Permissao = permissao;
        }
    }
}
