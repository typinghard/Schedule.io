using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Validations
{
    public class NovoLocalValidation : EntityValidation<Local>
    {
        public NovoLocalValidation()
        {
            RuleFor(l => l.NomeLocal)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome do Local não informado.");

            RuleFor(l => l.Descricao)
                .NotEmpty()
                .WithMessage("Descrição não informada.");

            RuleFor(l => l.ReservaLocal)
                .NotNull()
                .WithMessage("Reserva do local não informado.");

            RuleFor(l => l.LotacaoMaxima)
                .Must(ValidaLotacaoMaxima)
                .WithMessage("Reserva do local não informado.");
        }

        protected static bool ValidaLotacaoMaxima(int lotacaoMaxima)
        {
            return lotacaoMaxima < 0 ? false : true;
        }
    }
}
