using Bogus;
using System.Linq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Text;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Models;

namespace Schedule.io.Core.Test.Testes_Unitários.Models
{
    public class AgendaUsuarioTest
    {
        private AgendaUsuario agendaUsuario;

        public AgendaUsuarioTest()
        {
            agendaUsuario = new Faker<AgendaUsuario>("pt_BR")
                .CustomInstantiator((f) => new AgendaUsuario(f.Random.Guid().ToString(), f.Random.Guid().ToString()))
                .Generate(1)
                .First();
        }

        [Fact(DisplayName = "AgendaUsuario - DefinirUsuarioId - UsuarioId deve ser definido")]

        public void AgendaUsuario_DefinirUsuarioId_UsuarioIdDeveSerDefinido()
        {
            //Arrange
            var novoUsuarioId = Guid.NewGuid().ToString();

            //Act
            agendaUsuario.DefinirUsuarioId(novoUsuarioId);

            //Assert
            Assert.Equal(novoUsuarioId, agendaUsuario.UsuarioId);
        }

        [Fact(DisplayName = "AgendaUsuario - DefinirUsuarioId - UsuarioId deve ser inválido por ser vazio.")]
        public void AgendaUsuario_DefinirUsuarioId_UsuarioIdDeveSerInvalidoPorSerVazio()
        {
            //Arrange
            var novoEventoId = "";

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => agendaUsuario.DefinirUsuarioId(novoEventoId));

            //Assert
            Assert.Equal("Por favor, certifique-se que adicinou uma pessoa.", exception.Message);
        }


        [Fact(DisplayName = "AgendaUsuario - DefinirAgendaId - AgendaId deve ser definido")]
        public void AgendaUsuario_DefinirAgendaId_AgendaIdDeveSerDefinido()
        {
            //Arrange
            var novoAgendaId = Guid.NewGuid().ToString();

            //Act
            agendaUsuario.DefinirAgendaId(novoAgendaId);

            //Assert
            Assert.Equal(novoAgendaId, agendaUsuario.AgendaId);
        }

        [Fact(DisplayName = "AgendaUsuario - DefinirAgendaId - AgendaId deve ser inválido por ser vazio.")]
        public void AgendaUsuario_DefinirAgendaId_AgendaIdDeveSerInvalidoPorSerVazio()
        {
            //Arrange
            var novoAgendaId = "";

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => agendaUsuario.DefinirAgendaId(novoAgendaId));

            //Assert
            Assert.Equal("Por favor, certifique-se que adicinou uma agenda.", exception.Message);
        }


        [Fact(DisplayName = "AgendaUsuario - NovaAgendaUsuarioEhValida - Deve Ser Valido")]
        public void AgendaUsuario_NovaAgendaUsuarioEhValida_DeveSerValido()
        {
            //Act
            var ehValido = agendaUsuario.NovaAgendaUsuarioEhValido()/*.IsValid*/;

            //Assert
            Assert.True(ehValido);
        }

        [Fact(DisplayName = "AgendaUsuario - NovaAgendaUsuarioEhValida - Deve Ser Inválido")]
        public void AgendaUsuario_NovaAgendaUsuarioEhValida_DeveSerInvalido()
        {
            //Arrange
            var exception = Assert.Throws<ScheduleIoException>(() => agendaUsuario = new Faker<AgendaUsuario>("pt_BR")
                                                                                    .CustomInstantiator((f) => new AgendaUsuario("", ""))
                                                                                    .Generate(1)
                                                                                    .First());

            //Act
            var validacao = exception.Message.Split(',').ToList();

            //Assert
            Assert.Contains(validacao, x => x.Contains("UsuarioId não informado!"));
            Assert.Contains(validacao, x => x.Contains("AgendaId não informado!"));
        }

    }
}
