using Agenda.Domain.Commands;
using FluentValidation;
using System;

namespace Agenda.Domain.Validations
{
    public class EventoUsuarioValidacao<T> : AbstractValidator<T> where T : EventoUsuarioCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
