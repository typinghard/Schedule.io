using FluentValidation;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Validations.TipoEventoValidation
{
    public class TipoEventoValidation : EntityValidation<TipoEvento>
    {
        public TipoEventoValidation()
        {
            RuleFor(e => e.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, certifique-se que digitou um {PropertyName} para o Tipo do Evento.")
                .Length(2, 120).WithMessage("O {PropertyName} do Tipo do Evento deve ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(e => e.Descricao)
                .Length(2, 500).WithMessage("O {PropertyName} do Tipo do Evento deve ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}
