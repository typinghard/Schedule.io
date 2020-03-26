using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Models;
using FluentValidation;

namespace Agenda.Domain.Validations
{
    public class NovoLocalValidation : EntityValidation<Local>
    {
        public NovoLocalValidation()
        {
            RuleFor(l => l.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome do Local não informado.");

            RuleFor(l => l.Reserva)
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
