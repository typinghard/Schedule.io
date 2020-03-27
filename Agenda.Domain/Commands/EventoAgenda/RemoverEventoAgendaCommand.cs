using System;
using Agenda.Domain.Validations;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
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
