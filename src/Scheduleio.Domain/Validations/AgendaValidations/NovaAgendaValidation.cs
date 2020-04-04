using FluentValidation;
using Schedule.io.Core.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Validations.AgendaValidations
{
    public class NovaAgendaValidation : EntityValidation<Models.Agenda>
    {
        public NovaAgendaValidation()
        {
            RuleFor(a => a.Titulo)
                .NotNull()
                .NotEmpty()
                .WithMessage("Título não informado.");

            RuleFor(a => a.Publico)
                .NotNull()
                .WithMessage("Agenda público/privado não pode ser nulo.");
        }
    }
}
