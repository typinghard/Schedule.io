using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Agenda.Domain.Validations
{
    public class NovoUsuarioValidation : EntityValidation<Usuario>
    {
        public NovoUsuarioValidation()
        {
            RuleFor(u => u.Email)
                  .NotEmpty()
                  .WithMessage("E-mail não informado.")
                  .Must(ValidaFormatoEmail)
                  .WithMessage("Por favor, certifique-se que digitou um e-mail válido.");
        }

        /// <summary>
        /// Valida se o formato do e-mail está correto. Regex vindo do site da microsoft.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <see cref="https://docs.microsoft.com/pt-br/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format"/>
        protected static bool ValidaFormatoEmail(string email)
        {
            return Regex.IsMatch(email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
        }
    }
}
