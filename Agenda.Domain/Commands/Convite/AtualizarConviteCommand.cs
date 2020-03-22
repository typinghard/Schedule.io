using Agenda.Domain.Enums;
using Agenda.Domain.Models;
using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class AtualizarConviteCommand : ConviteCommand
    {
        public AtualizarConviteCommand(Guid id, Guid eventoId, Guid usuarioId, EnumStatusConviteEvento status, PermissoesConvite permissoes)
        {
            this.Id = id;
            this.EventoId = eventoId;
            this.UsuarioId = usuarioId;
            this.Status = status;
            this.Permissoes = permissoes;
        }


        public override bool EhValido()
        {
            ValidationResult = new AtualizarConviteCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
