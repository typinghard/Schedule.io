using FluentValidation.Results;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Helpers;
using Schedule.io.Validations.UsuarioValidations;
using System.Linq;

namespace Schedule.io.Models.AggregatesRoots
{
    public class Usuario : Entity, IAggregateRoot
    {
        public string Email { get; private set; }

        public Usuario(string email)
        {
            this.Email = email.ToLower();

            var resultadoValidacao = this.NovoUsuarioEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(string.Join(", ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        private Usuario()
        {

        }

        public void DefinirEmail(string email)
        {
            email = email.ToLower();
            if (email.EhVazio())
                throw new ScheduleIoException("Por favor, certifique-se que digitou um e-mail.");

            if (!email.EmailEhValido())
                throw new ScheduleIoException("Por favor, informe um e-mail válido.");

            this.Email = email;
        }

        private ValidationResult NovoUsuarioEhValido()
        {
            return new UsuarioValidation().Validate(this);
        }
    }
}
