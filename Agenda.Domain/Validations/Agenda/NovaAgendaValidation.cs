using Agenda.Domain.Core.DomainObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Validations
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
