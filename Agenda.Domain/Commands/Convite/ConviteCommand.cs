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
        public Guid Id { get; protected set; }
        public Guid EventoId { get; protected set; }
        public Guid UsuarioId { get; protected set; }
        public EnumStatusConviteEvento Status { get; protected set; }

        public PermissoesConvite Permissoes { get; protected set; }
    }
}
