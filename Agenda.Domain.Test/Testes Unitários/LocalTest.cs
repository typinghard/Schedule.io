using Agenda.Domain.Models;
using Bogus;
using System.Linq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Test
{
    public class LocalTest
    {
        private Local local;

        public LocalTest()
        {
            local = new Faker<Local>("pt_BR")
                .CustomInstantiator((f) => new Local(f.Random.Guid().ToString(), f.Random.String(200, 'a', 'z'), f.Random.String(500, 'a', 'z'), f.Random.Bool(), f.Random.Int(0)))
                .Generate(1)
                .First();
        }

        [Fact(DisplayName = "Local - DefinirIdentificadorExterno - Identificador Externo deve ser alterado")]
        public void Local_DefinirIdentificadorExterno_IdentificadorExternoDeveSerAlterado()
        {
            //Arrange
            var novoIdentificadorExterno = "podeserqualquerconvertidoemstring";

            //Act
            local.DefinirIdentificadorExterno(novoIdentificadorExterno);

            //Assert
            Assert.Equal(novoIdentificadorExterno, local.IdentificadorExterno);
        }

        [Fact(DisplayName = "Local - DefinirNomeLocal - Nome Local deve ser alterado")]
        public void Local_DefinirNomeLocal_NomeLocalDeveSerAlterado()
        {
            //Arrange
            var novoNomeLocal = "Novo nome do local";

            //Act
            local.DefinirNomeLocal(novoNomeLocal);

            //Assert
            Assert.Equal(novoNomeLocal, local.NomeLocal);
        }


        [Fact(DisplayName = "Local - DefinirDescricao - Descrição deve ser alterada")]
        public void Local_DefinirDescricao_DescricaoDeveSerAlterado()
        {
            //Arrange
            var novaDescricao = "Nova descrição do local";

            //Act
            local.DefinirDescricao(novaDescricao);

            //Assert
            Assert.Equal(novaDescricao, local.Descricao);
        }

        [Fact(DisplayName = "Local - ReservarLocal - O Local Deve Ser Reservado")]
        public void Local_ReservarLocal_DeveSerReservado()
        {
            //Arrange
            bool localReserva = true;

            //Act
            local.ReservarLocal();

            //Assert
            Assert.Equal(localReserva, local.ReservaLocal);
        }

        [Fact(DisplayName = "Local - RemoverReservaLocal - A Reserva Deve Ser Removida")]
        public void Local_RemoverReservaLocal_AReservaDeveSerRemovida()
        {
            //Arrange
            bool localReserva = false;

            //Act
            local.RemoverReservaLocal();

            //Assert
            Assert.Equal(localReserva, local.ReservaLocal);
        }

        [Fact(DisplayName = "Local - DefinirLotacaoMaxima - Deve Ser Valido")]
        public void Local_DefinirLotacaoMaxima_DeveSerValido()
        {
            //Arrange
            int lotacaoMaxima = 5;

            //Act
            local.DefinirLotacaoMaxima(lotacaoMaxima);

            //Assert
            Assert.Equal(lotacaoMaxima, local.LotacaoMaxima);
        }

        [Fact(DisplayName = "Local - NovalocalEhValido - Deve Ser Valido")]
        public void Local_NovoLocalEhValido_DeveSerValido()
        {
            //Act
            var ehValido = local.NovoLocalEhValido().IsValid;

            //Assert
            Assert.True(ehValido);
        }

        [Fact(DisplayName = "Local - NovoLocalEhValidO - Deve Ser Inválido")]
        public void Local_NovaLocalEhValido_DeveSerInvalido()
        {
            //Arrange
            local = new Faker<Local>("pt_BR")
                .CustomInstantiator((f) => new Local("", "", "", f.Random.Bool(), -122))
                .Generate(1)
                .First();

            //Act
            var ehValido = local.NovoLocalEhValido().IsValid;

            //Assert
            Assert.False(ehValido);
        }
    }
}
