using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
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
