using FluentValidation;
using System;
using Schedule.io.Core.Commands.ConviteCommands;

namespace Schedule.io.Core.Validations.ConviteValidations
{
    public class ConviteValidacao<T> : AbstractValidator<T> where T : ConviteCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
