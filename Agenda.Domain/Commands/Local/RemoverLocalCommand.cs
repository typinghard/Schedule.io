using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class RemoverLocalCommand : LocalCommand
    {
        public RemoverLocalCommand(Guid id)
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
