using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class RemoverAgendaCommand : AgendaCommand
    {
        public RemoverAgendaCommand(Guid id)
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
