using Schedule.io.Core.Validations.UsuarioValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.Usuario
{
   public  class RemoverUsuarioCommand : UsuarioCommand
    {
        public RemoverUsuarioCommand(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverUsuarioCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
