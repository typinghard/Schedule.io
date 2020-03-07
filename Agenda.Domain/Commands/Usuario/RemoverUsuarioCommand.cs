using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
   public  class RemoverUsuarioCommand : UsuarioCommand
    {
        public RemoverUsuarioCommand(Guid id)
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
