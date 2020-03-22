using Agenda.Domain.Enums;
using Agenda.Domain.Models;
using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class RegistrarConviteCommand : ConviteCommand
    {
        public RegistrarConviteCommand(Guid eventoId, Guid usuarioId, EnumStatusConviteEvento status, PermissoesConvite permissoes)
        {
            this.EventoId = eventoId;
            this.UsuarioId = usuarioId;
            this.Status = status;
            this.Permissoes = permissoes;
        }


        public override bool EhValido()
        {
            ValidationResult = new RegistrarConviteCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
