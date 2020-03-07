using Agenda.Domain.Core.Messages;
using System;

namespace Agenda.Domain.Commands
{
    public class AgendaUsuarioCommand : Command
    {
        public Guid Id { get; protected set; }
        public Guid AgendaId { get; protected set; }
        public Guid UsuarioId { get; protected set; }
    }
}
