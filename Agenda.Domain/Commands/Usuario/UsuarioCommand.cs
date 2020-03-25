using Agenda.Domain.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class UsuarioCommand : Command
    {
        public string Id { get; protected set; }
        public string UsuarioEmail { get; protected set; }
    }
}
