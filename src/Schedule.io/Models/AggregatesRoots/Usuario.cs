using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Text;
using System.Linq;
using Schedule.io.Core.Core.DomainObjects;
using FluentValidation;
using Schedule.io.Core.Validations.UsuarioValidations;

namespace Schedule.io.Models.AggregatesRoots
{
    public class Usuario : Entity, IAggregateRoot
    {
        public string Email { get; private set; }

        public Usuario(string id, string email) : base(id)
        {
            this.Email = email;

            var resultadoValidacao = this.NovoUsuarioEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(string.Join(", ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        public void DefinirEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ScheduleIoException("Por favor, certifique-se que digitou um e-mail válido.");
            }

            this.Email = email.ToLower();
        }

        public ValidationResult NovoUsuarioEhValido()
        {
            return new NovoUsuarioValidation().Validate(this);
        }
    }
}
