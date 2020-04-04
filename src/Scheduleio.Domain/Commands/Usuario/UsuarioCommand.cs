using Schedule.io.Core.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.Usuario
{
    public class UsuarioCommand : Command
    {
        public string Id { get; protected set; }
        public string UsuarioEmail { get; protected set; }
    }
}
