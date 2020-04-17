using Bogus;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Models.AggregatesRoots;
using System.Linq;
using Xunit;

namespace Schedule.io.Test.Testes_Unitários.Models
{
    public class LocalTest
    {
        private Local local;

        public LocalTest()
        {
            local = new Faker<Local>("pt_BR")
                .CustomInstantiator((f) => new Local(f.Random.String(200, 'a', 'z')))
                .Generate(1)
                .First();
        }

        [Fact(DisplayName = "Local - DefinirNomeLocal - Nome Local deve ser alterado")]
        public void Local_DefinirNomeLocal_NomeLocalDeveSerAlterado()
        {
            //Arrange
            var novoNomeLocal = new Faker().Random.String(199, 'a', 'z');

            //Act
            local.DefinirNomeLocal(novoNomeLocal);

            //Assert
            Assert.Equal(novoNomeLocal, local.Nome);
        }

        [Fact(DisplayName = "Local - DefinirNomeLocal - Nome Local deve ser inválido pelo tamanho")]
        public void Local_DefinirNomeLocal_NomeLocalDeveSerInvalidoPeloTamanho()
        {
            //Arrange
            var nomelocalInvalido = new Faker().Random.String(201, 'a', 'z');

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => local.DefinirNomeLocal(nomelocalInvalido));

            //Assert
            Assert.Equal("O nome do local deve ter entre 2 e 200 caracteres.", exception.Message);
        }


        [Fact(DisplayName = "Local - DefinirIdentificadorExterno - Identificador Externo deve ser alterado")]
        public void Local_DefinirIdentificadorExterno_IdentificadorExternoDeveSerAlterado()
        {
            //Arrange
            var novoIdentificadorExterno = new Faker().Random.String(199, 'a', 'z'); ;

            //Act
            local.DefinirIdentificadorExterno(novoIdentificadorExterno);

            //Assert
            Assert.Equal(novoIdentificadorExterno, local.IdentificadorExterno);
        }

        [Fact(DisplayName = "Local - DefinirIdentificadorExterno - Identificador Externo deve ser alterado mesmo vazio")]
        public void Local_DefinirIdentificadorExterno_IdentificadorExternoDeveSerAlteradoMesmoVazio()
        {
            //Arrange
            var novoIdentificadorExterno = string.Empty;

            //Act
            local.DefinirIdentificadorExterno(novoIdentificadorExterno);

            //Assert
            Assert.Equal(novoIdentificadorExterno, local.IdentificadorExterno);
        }

        [Fact(DisplayName = "Local - DefinirIdentificadorExterno - Identificador Externo deve ser inválido pelo tamanho")]
        public void Local_DefinirIdentificadorExterno_IdentificadorExternoDeveSerInvalidoPeloTamanho()
        {            //Arrange
            var novoIdentificadorExternoInvalido = new Faker().Random.String(1, 'a', 'z');

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => local.DefinirIdentificadorExterno(novoIdentificadorExternoInvalido));

            //Assert
            Assert.Equal("O Identificador Extorno deve ter entre 2 e 200 caracteres.", exception.Message);

        }


        [Fact(DisplayName = "Local - DefinirDescricao - Descrição deve ser alterada")]
        public void Local_DefinirDescricao_DescricaoDeveSerAlterado()
        {
            //Arrange
            var novaDescricao = new Faker().Random.String(499, 'a', 'z');

            //Act
            local.DefinirDescricao(novaDescricao);

            //Assert
            Assert.Equal(novaDescricao, local.Descricao);
        }

        [Fact(DisplayName = "Local - DefinirDescricao - Descrição deve ser alterada memso vazio")]
        public void Local_DefinirDescricao_DescricaoDeveSerAlteradoMesmoVazio()
        {
            //Arrange
            var novaDescricao = string.Empty;

            //Act
            local.DefinirDescricao(novaDescricao);

            //Assert
            Assert.Equal(novaDescricao, local.Descricao);
        }

        [Fact(DisplayName = "Local - DefinirDescricao - Descrição deve ser invalido pelo tamanho")]
        public void Local_DefinirDescricao_DescricaoDeveSerInvalidoPeloTamanho()
        {
            //Arrange
            var novaDescricao = new Faker().Random.String(501, 'a', 'z');

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => local.DefinirDescricao(novaDescricao));

            //Assert
            Assert.Equal("A descrição do local deve ter entre 2 e 500 caracteres.", exception.Message);
        }


        [Fact(DisplayName = "Local - ReservarLocal - O Local Deve Ser Reservado")]
        public void Local_ReservarLocal_DeveSerReservado()
        {
            //Arrange
            bool localReserva = true;

            //Act
            local.ReservarLocal();

            //Assert
            Assert.Equal(localReserva, local.Reserva);
        }


        [Fact(DisplayName = "Local - RemoverReservaLocal - A Reserva Deve Ser Removida")]
        public void Local_RemoverReservaLocal_AReservaDeveSerRemovida()
        {
            //Arrange
            bool localReserva = false;

            //Act
            local.RemoverReservaLocal();

            //Assert
            Assert.Equal(localReserva, local.Reserva);
        }


        [Fact(DisplayName = "Local - DefinirLotacaoMaxima - Deve Ser Valido")]
        public void Local_DefinirLotacaoMaxima_DeveSerValido()
        {
            //Arrange
            int lotacaoMaxima = new Faker().Random.Int(0, 100);

            //Act
            local.DefinirLotacaoMaxima(lotacaoMaxima);

            //Assert
            Assert.Equal(lotacaoMaxima, local.LotacaoMaxima);
        }

        [Fact(DisplayName = "Local - DefinirLotacaoMaxima - Deve Ser Invalido")]
        public void Local_DefinirLotacaoMaxima_DeveSerInvalido()
        {
            //Arrange
            int lotacaoMaxima = new Faker().Random.Int(-100, -1);

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => local.DefinirLotacaoMaxima(lotacaoMaxima));

            //Assert
            Assert.Equal("Por favor, certifique-se qua a lotação máxima de usuários para o local não é menor que 0.", exception.Message);
        }


        [Fact(DisplayName = "Local - NovalocalEhValido - Deve Ser Valido")]
        public void Local_NovoLocalEhValido_DeveSerValido()
        {
            //Act
            var ehValido = local.LocalEhValido().IsValid;

            //Assert
            Assert.True(ehValido);
        }

        [Fact(DisplayName = "Local - NovoLocalEhValidO - Deve Ser Inválido")]
        public void Local_NovaLocalEhValido_DeveSerInvalido()
        {
            //Arrange
            var exception = Assert.Throws<ScheduleIoException>(() => local = new Faker<Local>("pt_BR")
                                                                        .CustomInstantiator((f) => new Local(""))
                                                                        .Generate(1)
                                                                        .First());


            //Act
            var validacao = exception.Message.Split("##").ToList();

            //Assert
            Assert.Equal("Nome do Local não informado.", exception.Message);
        }
    }
}
