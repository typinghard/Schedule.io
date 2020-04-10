using FluentValidation;
using Schedule.io.Core.Commands.LocalCommands;
using System;

namespace Schedule.io.Core.Validations.LocalValidations
{
    public abstract class LocalValidacao<T> : AbstractValidator<T> where T : LocalCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
