using Agenda.Domain.Models;
using Bogus;
using System.Linq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Test
{
    public class AgendaUsuarioTest
    {
        private AgendaUsuario agendaUsuario;

        public AgendaUsuarioTest()
        {
            agendaUsuario = new Faker<AgendaUsuario>("pt_BR")
                .CustomInstantiator((f) => new AgendaUsuario(f.Random.Guid(), f.Random.Guid()))
                .Generate(1)
                .First();
        }

        //DefinirUsuarioId
        //    DefinirAgendaId

        [Fact(DisplayName = "AgendaUsuario - DefinirUsuarioId - UsuarioId deve ser definido")]

        public void AgendaUsuario_DefinirUsuarioId_UsuarioIdDeveSerDefinido()
        {
            //Arrange
            var novoUsuarioId = Guid.NewGuid();

            //Act
            agendaUsuario.DefinirUsuarioId(novoUsuarioId);

            //Assert
            Assert.Equal(novoUsuarioId, agendaUsuario.UsuarioId);
        }

        [Fact(DisplayName = "AgendaUsuario - DefinirAgendaId - AgendaId deve ser definido")]

        public void AgendaUsuario_DefinirAgendaId_AgendaIdDeveSerDefinido()
        {
            //Arrange
            var novoAgendaId = Guid.NewGuid();

            //Act
            agendaUsuario.DefinirAgendaId(novoAgendaId);

            //Assert
            Assert.Equal(novoAgendaId, agendaUsuario.AgendaId);
        }

        [Fact(DisplayName = "AgendaUsuario - NovaAgendaUsuarioEhValida - Deve Ser Valido")]
        public void AgendaUsuario_NovaAgendaUsuarioEhValida_DeveSerValido()
        {
            //Act
            var ehValido = agendaUsuario.NovaAgendaUsuarioEhValido().IsValid;

            //Assert
            Assert.True(ehValido);
        }

        [Fact(DisplayName = "AgendaUsuario - NovaAgendaUsuarioEhValida - Deve Ser Inválido")]
        public void AgendaUsuario_NovaAgendaUsuarioEhValida_DeveSerInvalido()
        {
            //Arrange
            agendaUsuario = new Faker<AgendaUsuario>("pt_BR")
            .CustomInstantiator((f) => new AgendaUsuario(Guid.Empty, Guid.Empty))
            .Generate(1)
            .First();

            //Act
            var ehValido = agendaUsuario.NovaAgendaUsuarioEhValido().IsValid;

            //Assert
            Assert.False(ehValido);
        }

    }
}
