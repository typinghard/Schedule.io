using Schedule.io.Core.Validations.ConviteValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.Convite
{
    public class RemoverConviteCommand : ConviteCommand
    {
        public RemoverConviteCommand(string id)
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
