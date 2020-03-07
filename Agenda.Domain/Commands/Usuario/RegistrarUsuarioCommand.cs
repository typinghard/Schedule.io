using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
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
