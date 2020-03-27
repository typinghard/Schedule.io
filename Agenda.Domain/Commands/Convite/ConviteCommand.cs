using Agenda.Domain.Core.Messages;
using Agenda.Domain.Enums;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class ConviteCommand : Command
    {
        public string Id { get; protected set; }
        public string UsuarioId { get; protected set; }
        public string EventoId { get; protected set; }
        public bool Confirmacao { get; protected set; }
        public EnumStatusConviteEvento Status { get; protected set; }
        public PermissoesConvite Permissoes { get; protected set; }
    }
}
