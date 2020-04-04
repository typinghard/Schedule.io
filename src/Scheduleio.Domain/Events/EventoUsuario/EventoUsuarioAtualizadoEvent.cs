using Schedule.io.Core.Core.Messages;
using Schedule.io.Core.Models;
using System;


namespace Schedule.io.Core.Events.EventoUsuario
{
    public class EventoUsuarioAtualizadoEvent : Event
    {
        public string Id { get; set; }
        public string UsuarioId { get; set; }
        public bool Confirmacao { get; set; }
        public Permissao Permissao { get; set; }

        public EventoUsuarioAtualizadoEvent(string id, string usuarioId, bool confirmacao, Permissao permissao)
        {
            this.Id = id;
            this.UsuarioId = usuarioId;
            this.AggregateId = id;
            this.Confirmacao = confirmacao;
            this.Permissao = permissao;
        }
    }
}
