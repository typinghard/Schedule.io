using FluentValidation;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Models.AggregatesRoots;
using System.Text.RegularExpressions;

namespace Schedule.io.Validations.UsuarioValidations
{
    public class UsuarioValidation : EntityValidation<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(u => u.Email)
                  .NotEmpty()
                  .WithMessage("E-mail não informado.")
                  .EmailAddress()
                  .WithMessage("Por favor, certifique-se que digitou um e-mail válido.");
        }
    }
}
