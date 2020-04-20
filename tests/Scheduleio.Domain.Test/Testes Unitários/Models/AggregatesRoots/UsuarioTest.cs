﻿using Bogus;
using Xunit;
using System.Linq;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Core.DomainObjects;

namespace Schedule.io.Test.Testes_Unitários.Models.AggregatesRoots
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
        public void Usuario_NovoUsuarioEhValido_DeveSerValido()
        {
            //Act
            usuario = new Faker<Usuario>("pt_BR")
                                    .CustomInstantiator((f) => new Usuario(f.Person.Email.ToLower()))
                                    .Generate(1)
                                    .First();
        }

        [Fact(DisplayName = "Usuario - DefinirUsuarioEmail - O E-mail deve ser alterado.")]
        public void Usuario_DefinirUsuarioEmail_UsuarioEmailDeveSerAlterado()
        {
            //Arrange
            var novoEmail = new Faker().Person.Email;

            //Act
            usuario.DefinirEmail(novoEmail);

            //Assert
            Assert.Equal(novoEmail.ToLower(), usuario.Email);
        }

        [Fact(DisplayName = "Usuario - DefinirUsuarioEmail - O E-mail deve ser inválido por estar vazio.")]
        public void Usuario_DefinirUsuarioEmail_UsuarioEmailDeveSerInvalidoVazio()
        {
            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => usuario.DefinirEmail(""));

            //Assert
            Assert.Equal("Por favor, certifique-se que digitou um e-mail.", exception.Message);
        }

        [Fact(DisplayName = "Usuario - DefinirUsuarioEmail - O E-mail deve ser inválido por estar vazio.")]
        public void Usuario_DefinirUsuarioEmail_UsuarioEmailDeveSerInvalido()
        {
            //Arrange
            var novoEmail = "fdsa@fdsa.";

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => usuario.DefinirEmail(novoEmail));

            //Assert
            Assert.Equal("Por favor, informe um e-mail válido.", exception.Message);
        }

        [Fact(DisplayName = "Usuario - UsuarioEhValido - Deve Ser Inválido")]
        public void Usuario_UsuarioEhValido_DeveSerInvalido()
        {
            //Arrange
            var exception = Assert.Throws<ScheduleIoException>(() => new Faker<Usuario>("pt_BR")
                                                                .CustomInstantiator((f) => new Usuario(""))
                                                                .Generate(1)
                                                                .First());

            //Assert
            Assert.Equal("Por favor, certifique-se que digitou um e-mail válido.", exception.Message);
        }
    }
}
