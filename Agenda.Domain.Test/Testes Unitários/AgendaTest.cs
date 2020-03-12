﻿using Bogus;
using Xunit;
using Agenda.Domain.Core.DomainObjects;
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
                .CustomInstantiator((f) => new Models.Agenda(f.Random.String(150, 'a', 'z')))
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

        [Fact(DisplayName = "Agenda - DefinirTítulo - Título deve ser inválido por ser vazio")]
        public void Agenda_DefinirTitulo_TituloDeveSerInValidoPorSerVazio()
        {
            //Arrange
            var novoTituloInvalido = "";

            //Act
            var exception = Assert.Throws<DomainException>(() => agenda.DefinirTitulo(novoTituloInvalido));

            //Assert
            Assert.Equal("Por favor, certifique-se que digitou um título.", exception.Message);
        }

        [Fact(DisplayName = "Agenda - DefinirTítulo - Título deve ser inválido pelo tamanho")]
        public void Agenda_DefinirTitulo_TituloDeveSerInValidoPeloTamanho()
        {
            //Arrange
            var novoTitulo = new Faker().Random.String(151, 'a', 'z');


            //Act
            var exception = Assert.Throws<DomainException>(() => agenda.DefinirTitulo(novoTitulo));

            //Assert
            Assert.Equal("O título deve ter entre 2 e 150 caracteres.", exception.Message);
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

        [Fact(DisplayName = "Agenda - DefinirDescricao - Descrição deve ser inválido pelo tamanho")]
        public void Agenda_DefinirDescricao_DescricaoDeveSerInValidoPeloTamanho()
        {
            //Arrange
            var novaDescricaoInvalida = new Faker().Random.String(501, 'a', 'z');

            //Act
            var exception = Assert.Throws<DomainException>(() => agenda.DefinirDescricao(novaDescricaoInvalida));

            //Assert
            Assert.Equal("A descrição deve ter entre 2 e 500 caracteres.", exception.Message);
        }


        [Fact(DisplayName = "Agenda - TornarAgendaPublica - Agenda Deve Ser Pública")]
        public void Agenda_TornarAgendaPublica_AgendaDeveSerPublica()
        {
            //Act
            agenda.TornarAgendaPublica();

            //Assert
            Assert.True(agenda.Publico);
        }


        [Fact(DisplayName = "Agenda - TornarAgendaPrivada - Agenda Deve Ser Privada")]
        public void Agenda_TornarAgendaPrivada_AgendaDeveSerPrivada()
        {
            //Act
            agenda.TornarAgendaPrivado();

            //Assert
            Assert.False(agenda.Publico);
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
            //Act
            var exception = Assert.Throws<DomainException>(() => agenda = new Faker<Models.Agenda>("pt_BR")
                                                                        .CustomInstantiator((f) => new Models.Agenda(""))
                                                                        .Generate(1)
                                                                        .First());

            //Assert
            Assert.Contains("Título não informado.", exception.Message);
        }
    }
}
