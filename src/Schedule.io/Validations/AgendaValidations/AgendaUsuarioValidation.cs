using FluentValidation;
using Schedule.io.Models.ValueObjects;

namespace Schedule.io.Validations.AgendaValidations
{
    public class AgendaUsuarioValidation : AbstractValidator<AgendaUsuario>
    {
        public AgendaUsuarioValidation()
        {
            RuleFor(a => a.AgendaId)
                .NotNull()
                .NotEmpty()
                .WithMessage("AgendaId da Agenda do Usuario não informado.");

            RuleFor(a => a.UsuarioId)
                .NotNull()
                .NotEmpty()
                .WithMessage("UsuarioId da Agenda do Usuario não informado.");
        }
    }
}
