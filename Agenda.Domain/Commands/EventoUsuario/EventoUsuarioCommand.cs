using Agenda.Domain.Core.Messages;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class EventoUsuarioCommand : Command
    {
        public string Id { get; protected set; }
        public string UsuarioId { get; protected set; }
        public bool Confirmacao { get; protected set; }

        public Permissao Permissao { get; protected set; }
    }
}
