using Schedule.io.Core.Validations.AgendaValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.AgendaCommands
{
    public class RemoverAgendaCommand : AgendaCommand
    {
        public RemoverAgendaCommand(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverAgendaCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
