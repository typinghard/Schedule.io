using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Validations
{
    public class AgendaUsuarioValidation : EntityValidation<AgendaUsuario>
    {
        public AgendaUsuarioValidation()
        {
            RuleFor(a => a.UsuarioId)
                .NotEqual(Guid.Empty)
                .WithMessage("UsuarioId não informado!");

            RuleFor(a => a.AgendaId)
                .NotEqual(Guid.Empty)
                .WithMessage("AgendaId não informado!");
        }
    }
}
