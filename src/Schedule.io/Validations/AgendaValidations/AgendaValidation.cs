﻿using FluentValidation;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Models.AggregatesRoots;

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

            RuleFor(a => a.UsuarioIdCriador)
                .NotNull()
                .NotEmpty()
                .WithMessage("UsuarioIdCriador não informado.");

            RuleFor(a => a.Publico)
                .NotNull()
                .WithMessage("Agenda público/privado não pode ser nulo.");
        }
    }
}
