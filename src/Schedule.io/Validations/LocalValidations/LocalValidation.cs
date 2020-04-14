using FluentValidation;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Validations.LocalValidations
{
    public class LocalValidation : EntityValidation<Local>
    {
        public LocalValidation()
        {
            RuleFor(l => l.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} do Local não informado.");

            RuleFor(l => l.Reserva)
                .NotNull()
                .WithMessage("{PropertyName} do local não informado.");

            RuleFor(l => l.LotacaoMaxima)
                .GreaterThan(0)
                .WithMessage("{PropertyName} do local não informado.");
        }
    }
}
