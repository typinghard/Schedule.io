using Schedule.io.Core.Core.Messages;
using Schedule.io.Core.Enums;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.ConviteCommands
{
    public class ConviteCommand : Command
    {
        public string Id { get; protected set; }
        public string UsuarioId { get; protected set; }
        public string EmailConvidado { get; protected set; }
        public string EventoId { get; protected set; }
        public bool Confirmacao { get; protected set; }
        public EnumStatusConviteEvento Status { get; protected set; }
        public PermissoesConvite Permissoes { get; protected set; }
    }
}
