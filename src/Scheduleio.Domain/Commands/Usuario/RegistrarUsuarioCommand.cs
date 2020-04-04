using Schedule.io.Core.Validations.UsuarioValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.Usuario
{
    public class RegistrarUsuarioCommand : UsuarioCommand
    {
        public RegistrarUsuarioCommand(string usuarioEmail)
        {
            this.UsuarioEmail = usuarioEmail;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarUsuarioCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
