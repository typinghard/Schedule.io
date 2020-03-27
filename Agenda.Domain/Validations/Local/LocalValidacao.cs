using Agenda.Domain.Commands;
using FluentValidation;
using System;

namespace Agenda.Domain.Validations
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
