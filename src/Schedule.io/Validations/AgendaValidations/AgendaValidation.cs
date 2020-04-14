using FluentValidation;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Validations.AgendaValidations
{
    public class AgendaValidation : EntityValidation<Agenda>
    {
        public AgendaValidation()
        {
            RuleFor(a => a.Titulo)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não informado.");

            RuleFor(a => a.Publico)
                .NotNull()
                .WithMessage("Agenda público/privado não pode ser nulo.");
        }
    }
}
