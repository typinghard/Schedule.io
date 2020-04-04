using Schedule.io.Core.Validations.LocalValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.Local
{
    public class RemoverLocalCommand : LocalCommand
    {
        public RemoverLocalCommand(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverLocalCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
