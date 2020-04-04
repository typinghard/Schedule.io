using Schedule.io.Core.Core.Messages;
using System;

namespace Schedule.io.Core.Commands.AgendaUsuario
{
    public class AgendaUsuarioCommand : Command
    {
        public string Id { get; protected set; }
        public string AgendaId { get; protected set; }
        public string UsuarioId { get; protected set; }
    }
}
