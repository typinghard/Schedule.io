using Bogus;
using Xunit;
using Agenda.Domain.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Test
{
    public class AgendaTest
    {
        private Models.Agenda agenda;

        public AgendaTest()
        {
            agenda = new Faker<Models.Agenda>("pt_BR")
                .CustomInstantiator((f) => new Models.Agenda(f.Random.String(150, 'a', 'z'), f.Random.String(500, 'a', 'z'), f.Random.Bool()))
                .Generate(1)
                .First();
        }

        [Fact(DisplayName = "Agenda - DefinirTítulo - Título deve ser alterado")]
        public void Agenda_DefinirTitulo_TituloDeveSerAlterado()
        {
            //Arrange
            var novoTitulo = "Nova título da agenda";

            //Act
            agenda.DefinirTitulo(novoTitulo);

            //Assert
            Assert.Equal(novoTitulo, agenda.Titulo);
        }

        [Fact(DisplayName = "Agenda - DefinirDescricao - Descrição deve ser alterada")]
        public void Agenda_DefinirDescricao_DescricaoDeveSerAlterado()
        {
            //Arrange
            var novoDescricao = "Nova descrição da agenda";

            //Act
            agenda.DefinirDescricao(novoDescricao);

            //Assert
            Assert.Equal(novoDescricao, agenda.Descricao);
        }

        [Fact(DisplayName = "Agenda - TornarAgendaPublica - Agenda Deve Ser Pública")]
        public void Agenda_TornarAgendaPublica_AgendaDeveSerPublica()
        {
            //Arrange
            var tornarAgendaPublica = true;

            //Act
            agenda.TornarAgendaPublica();

            //Assert
            Assert.Equal(tornarAgendaPublica, agenda.Publico);
        }

        [Fact(DisplayName = "Agenda - TornarAgendaPrivada - Agenda Deve Ser Privada")]
        public void Agenda_TornarAgendaPrivada_AgendaDeveSerPrivada()
        {
            //Arrange
            var tornarAgendaPrivada = false;

            //Act
            agenda.TornarAgendaPrivado();

            //Assert
            Assert.Equal(tornarAgendaPrivada, agenda.Publico);
        }

        [Fact(DisplayName = "Agenda - NovaAgendaEhValida - Deve Ser Valido")]
        public void Agenda_NovaAgendaEhValida_DeveSerValido()
        {
            //Act
            var ehValido = agenda.NovaAgendaEhValida().IsValid;

            //Assert
            Assert.True(ehValido);
        }

        [Fact(DisplayName = "Agenda - NovaAgendaEhValida - Deve Ser Inválido")]
        public void Agenda_NovaAgendaEhValida_DeveSerInvalido()
        {
            //Arrange
            agenda = new Faker<Models.Agenda>("pt_BR")
                .CustomInstantiator((f) => new Models.Agenda("", "", false))
                .Generate(1)
                .First();

            //Act
            var ehValido = agenda.NovaAgendaEhValida().IsValid;

            //Assert
            Assert.False(ehValido);
        }
    }
}
