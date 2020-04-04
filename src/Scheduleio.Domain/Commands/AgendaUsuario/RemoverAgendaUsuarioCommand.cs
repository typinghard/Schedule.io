using Schedule.io.Core.Validations.AgendaUsuarioValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.AgendaUsuario
{
   public  class RemoverAgendaUsuarioCommand : AgendaUsuarioCommand
    {
        public RemoverAgendaUsuarioCommand(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverAgendaUsuarioCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
