using FluentValidation.Results;
using Schedule.io.Core.Enums;
using Schedule.io.Core.Models;
using Schedule.io.Core.Validations.ConviteValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.ConviteCommands
{
    public class AtualizarConviteCommand : ConviteCommand
    {
        public AtualizarConviteCommand(string id, string eventoId, string usuarioId, string emailConvidado, EnumStatusConviteEvento status, PermissoesConvite permissoes)
        {
            Id = id;
            EventoId = eventoId;
            UsuarioId = usuarioId;
            EmailConvidado = emailConvidado;
            Status = status;
            Permissoes = permissoes;
        }


        public override bool EhValido()
        {
            ValidationResult = new AtualizarConviteCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
