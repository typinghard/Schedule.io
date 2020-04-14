using FluentValidation.Results;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Validations.UsuarioValidations;
using System.Linq;

namespace Schedule.io.Models.AggregatesRoots
{
    public class Usuario : Entity, IAggregateRoot
    {
        public string Email { get; private set; }

        public Usuario(string email)
        {
            this.Email = email;

            var resultadoValidacao = this.UsuarioEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(string.Join(", ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        private Usuario()
        {

        }

        public void DefinirEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ScheduleIoException("Por favor, certifique-se que digitou um e-mail válido.");
            }

            this.Email = email.ToLower();
        }

        public ValidationResult UsuarioEhValido()
        {
            return new UsuarioValidation().Validate(this);
        }
    }
}
