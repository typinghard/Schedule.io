using Schedule.io.Core.Core.Messages;
using System;

namespace Schedule.io.Core.Commands.AgendaCommands
{
    public class AgendaCommand : Command
    {
        public string Id { get; protected set; }
        public string Titulo { get; protected set; }

        public string Descricao { get; protected set; }

        public bool Publico { get; protected set; }
    }
}
