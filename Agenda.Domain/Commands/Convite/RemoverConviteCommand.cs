using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class RemoverConviteCommand : ConviteCommand
    {
        public RemoverConviteCommand(Guid id)
        {
            this.Id = id;
            this.AggregateId = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverConviteCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
