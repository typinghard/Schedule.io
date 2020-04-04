using Schedule.io.Core.Enums;
using Schedule.io.Core.Models;
using Schedule.io.Core.Validations.ConviteValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.Convite
{
    public class RegistrarConviteCommand : ConviteCommand
    {
        public RegistrarConviteCommand(string eventoId, string usuarioId, EnumStatusConviteEvento status, PermissoesConvite permissoes)
        {
            EventoId = eventoId;
            UsuarioId = usuarioId;
            Permissoes = permissoes;
        }


        public override bool EhValido()
        {
            ValidationResult = new RegistrarConviteCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
