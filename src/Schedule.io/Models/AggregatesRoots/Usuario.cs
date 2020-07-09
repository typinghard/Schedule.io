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

        private Usuario() { }

        public Usuario(string email)
        {
            Email = email.ToLower();

            var resultadoValidacao = NovoUsuarioEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(resultadoValidacao.Errors.Select(x => x.ErrorMessage).ToList());
        }

        public void DefinirEmail(string email)
        {
            email = email.ToLower();
            if (!email.EmailEhValido())
                throw new ScheduleIoException("Por favor, informe um e-mail válido.");

            Email = email;
        }

        private ValidationResult NovoUsuarioEhValido()
        {
            return new UsuarioValidation().Validate(this);
        }
    }
}
