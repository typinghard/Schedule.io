using Schedule.io.Core.Validations.UsuarioValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.UsuarioCommands
{
    public class AtualizarUsuarioCommand : UsuarioCommand
    {
        public AtualizarUsuarioCommand(string id, string usuarioEmail)
        {
            this.Id = id;
            this.UsuarioEmail = usuarioEmail;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarUsuarioCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
