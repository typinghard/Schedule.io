using Agenda.Domain.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Agenda.Domain.Models
{
    public class Usuario : Entity, IAggregateRoot
    {
        //ESSE?
        //[DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string UsuarioEmail { get; private set; }

        public Usuario(string usuarioEmail)
        {
            this.UsuarioEmail = usuarioEmail;
        }

        public void DefinirEmail(string email)
        {
            if (string.IsNullOrEmpty(email) && ValidaFormatoEmail(email))
            {
                throw new DomainException("Por favor, certifique-se que digitou um e-mail válido.");
            }

            this.UsuarioEmail = email;
        }

        //OU ESSE?
        private bool ValidaFormatoEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"b[A-Z0-9._%-]+@[A-Z0-9.-]+.[A-Z]{2,4}b");
        }
    }
}
