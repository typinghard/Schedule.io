using Schedule.io.Core.Validations.EventoAgendaValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.EventoAgendaCommands
{
    public class RemoverEventoAgendaCommand : EventoAgendaCommand
    {
        public RemoverEventoAgendaCommand(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverEventoAgendaCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
