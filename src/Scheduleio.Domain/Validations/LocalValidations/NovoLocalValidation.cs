using FluentValidation;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Models;

namespace Schedule.io.Core.Validations.LocalValidations
{
    public class NovoLocalValidation : EntityValidation<Local>
    {
        public NovoLocalValidation()
        {
            RuleFor(l => l.NomeLocal)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome do Local não informado.");

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
