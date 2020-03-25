using Agenda.Domain.Core.Messages;
using System;

namespace Agenda.Domain.Commands
{
    public class AgendaUsuarioCommand : Command
    {
        public string Id { get; protected set; }
        public string AgendaId { get; protected set; }
        public string UsuarioId { get; protected set; }
    }
}
