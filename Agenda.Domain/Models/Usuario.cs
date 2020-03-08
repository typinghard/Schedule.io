using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Helpers;
using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Text;

namespace Agenda.Domain.Models
{
    public class Usuario : Entity, IAggregateRoot
    {
        public string UsuarioEmail { get; private set; }

        public Usuario(string usuarioEmail)
        {
            this.UsuarioEmail = usuarioEmail;
        }

        public void DefinirEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new DomainException("Por favor, certifique-se que digitou um e-mail válido.");
            }

            this.UsuarioEmail = email.ToLower();
        }

        public ValidationResult NovoUsuarioEhValido()
        {
            return new NovoUsuarioValidation().Validate(this);
        }
    }
}
