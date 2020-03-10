using Bogus;
using Xunit;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Agenda.Domain.Core.DomainObjects;

namespace Agenda.Domain.Test
{
    public class UsuarioTest
    {
        private Usuario usuario;

        public UsuarioTest()
        {
            usuario = new Faker<Usuario>("pt_BR")
                .CustomInstantiator((f) => new Usuario(f.Person.Email.ToLower()))
                .Generate(1)
                .First();
        }

        [Fact(DisplayName = "Usuario - NovoUsuarioEhValido - Deve Ser Valido")]
        public void Usuario_NovoaUsuarioEhValido_DeveSerValido()
        {
            //Act
            var ehValido = usuario.NovoUsuarioEhValido().IsValid;

            //Assert
            Assert.True(ehValido);
        }

        [Fact(DisplayName = "Usuario - DefinirUsuarioEmail - O E-mail deve ser alterado.")]
        public void Usuario_DefinirUsuarioEmail_UsuarioEmailDeveSerAlterado()
        {
            //Arrange
            var novoEmail = "abc@teste.com.br";

            //Act
            usuario.DefinirEmail(novoEmail);

            //Assert
            Assert.Equal(novoEmail, usuario.Email);
        }

        [Fact(DisplayName = "Usuario - DefinirUsuarioEmail - O E-mail deve ser inválido por estar vazio.")]
        public void Usuario_DefinirUsuarioEmail_UsuarioEmailDeveSerInvalidoVazio()
        {
            //Act
            var exception = Assert.Throws<DomainException>(() => usuario.DefinirEmail(""));

            //Assert
            Assert.Equal("Por favor, certifique-se que digitou um e-mail válido.", exception.Message);
        }

        [Fact(DisplayName = "Usuario - NovoUsuarioEhValido - Deve Ser Inválido")]
        public void Usuario_NovaAgendaEhValida_DeveSerInvalido()
        {
            //Arrange
            var exception = Assert.Throws<DomainException>(() => new Faker<Usuario>("pt_BR")
                                                                .CustomInstantiator((f) => new Usuario(f.Person.Email + "123"))
                                                                .Generate(1)
                                                                .First());

            //Assert
            Assert.Equal("Por favor, certifique-se que digitou um e-mail válido.", exception.Message);
        }
    }
}
