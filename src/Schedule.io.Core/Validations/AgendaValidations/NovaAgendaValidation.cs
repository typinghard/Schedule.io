using FluentValidation;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Validations.AgendaValidations
{
    public class NovaAgendaValidation : EntityValidation<Agenda>
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
