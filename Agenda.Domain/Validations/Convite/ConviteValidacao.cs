using Agenda.Domain.Commands;
using FluentValidation;
using System;

namespace Agenda.Domain.Validations
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
