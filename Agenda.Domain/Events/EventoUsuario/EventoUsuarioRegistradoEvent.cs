using Agenda.Domain.Core.Messages;
using Agenda.Domain.Models;
using System;

namespace Agenda.Domain.Events
{
    public class EventoUsuarioRegistradoEvent : Event
    {
        public string Id { get; set; }
        public string UsuarioId { get; set; }
        public bool Confirmacao { get; set; }
        public Permissao Permissao { get; set; }

        public EventoUsuarioRegistradoEvent(string id, string usuarioId, bool confirmacao, Permissao permissao)
        {
            this.Id = id;
            this.UsuarioId = usuarioId;
            this.AggregateId = id;
            this.Confirmacao = confirmacao;
            this.Permissao = permissao;
        }
    }
}
