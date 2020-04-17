using Bogus;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Models.AggregatesRoots;
using System.Linq;
using Xunit;

namespace Schedule.io.Test.Testes_Unitários.Models
{
    public class TipoEventoTest
    {
        private TipoEvento tipoEvento;

        public TipoEventoTest()
        {
            tipoEvento = new Faker<TipoEvento>("pt_BR")
                .CustomInstantiator((f) => new TipoEvento(f.Random.String(120, 'a', 'z'),
                                                          f.Random.String(500, 'a', 'z')))
                .Generate(1)
                .First();
        }

        [Fact(DisplayName = "TipoEvento - Definir Nome - Nome do Tipo Evento deve ser alterado")]
        public void TipoEvento_DefinirNome_NomeTipoEventoDeveSerAlterado()
        {
            //Arrange
            var novoNome = new Faker().Random.String(120, 'a', 'z');

            //Act
            tipoEvento.DefinirNome(novoNome);

            //Assert
            Assert.Equal(novoNome, tipoEvento.Nome);
        }

        [Fact(DisplayName = "TipoEvento - Definir Nome - Nome do Tipo Evento deve ser invalido por ser vazio")]
        public void TipoEvento_DefinirNome_NomeTipoEventoDeveSerInvalidoPorSerVazio()
        {
            //Arrange
            var novoNome = string.Empty;

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => tipoEvento.DefinirNome(novoNome));

            //Assert
            Assert.Equal("Por favor, certifique-se que digitou um nome para o tipo do evento.", exception.Message);
        }

        [Fact(DisplayName = "TipoEvento - Definir Nome - Nome do Tipo Evento deve ser invalido por ser vazio")]
        public void TipoEvento_DefinirNome_NomeTipoEventoDeveSerInvalidoPeloTamanho()
        {
            //Arrange
            var novoNome = new Faker().Random.String(1, 'a', 'z');

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => tipoEvento.DefinirNome(novoNome));

            //Assert
            Assert.Equal("O nome do tipo do evento deve ter entre 2 e 120 caracteres.", exception.Message);
        }


        [Fact(DisplayName = "TipoEvento - DefinirDescricaoTipoEvento - Descrição do Tipo Evento deve ser alterado")]
        public void EventoAgenda_DefinirDescricaoTipoEvento_DescricaoDeveSerAlterado()
        {
            //Arrange
            var novaDescricao = new Faker().Random.String(500, 'a', 'z');

            //Act
            tipoEvento.DefinirDescricao(novaDescricao);

            //Assert
            Assert.Equal(novaDescricao, tipoEvento.Descricao);
        }

        [Fact(DisplayName = "TipoEvento - DefinirDescricaoTipoEvento - Descrição do Tipo Evento deve ser alterado mesmo vazio")]
        public void EventoAgenda_DefinirDescricaoTipoEvento_DescricaoDeveSerAlteradoMesmoVazio()
        {
            //Arrange
            var novaDescricao = string.Empty;

            //Act
            tipoEvento.DefinirDescricao(novaDescricao);

            //Assert
            Assert.Equal(novaDescricao, tipoEvento.Descricao);
        }

        [Fact(DisplayName = "TipoEvento - DefinirDescricaoTipoEvento - Descrição do Tipo Evento deve ser invalido pelo tamanho")]
        public void EventoAgenda_DefinirDescricaoTipoEvento_DescricaoDeveSerInvalidoPeloTamanho()
        {
            //Arrange
            var novaDescricao = new Faker().Random.String(501, 'a', 'z');

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => tipoEvento.DefinirDescricao(novaDescricao));

            //Assert
            Assert.Equal("A descrição deve ter entre 2 e 500 caracteres.", exception.Message);
        }

        [Fact(DisplayName = "TipoEvento - TipoEventoEhValido - Deve Ser Valido")]
        public void TipoEvento_TipoEventoEhValido_DeveSerValido()
        {
            //Act
            var ehValido = tipoEvento.TipoEventoEhValido();

            //Assert
            Assert.True(ehValido);
        }

        [Fact(DisplayName = "TipoEvento - TipoEventoEhValido - Deve Ser Inválido")]
        public void TipoEvento_TipoEventoEhValido_DeveSerInvalido()
        {
            //Arrange
            var exception = Assert.Throws<ScheduleIoException>(() => tipoEvento = new Faker<TipoEvento>("pt_BR")
                                                                        .CustomInstantiator((f) => new TipoEvento("", ""))
                                                                        .Generate(1)
                                                                        .First());

            var validacao = exception.Message.Split("## ").ToList();

            //Assert
            Assert.Contains(validacao, x => x.Contains("Por favor, certifique-se que digitou um Nome para o Tipo do Evento."));
            Assert.Contains(validacao, x => x.Contains("O Nome do Tipo do Evento deve ter entre 2 e 120 caracteres."));
            Assert.Contains(validacao, x => x.Contains("O Descricao do Tipo do Evento deve ter entre 2 e 500 caracteres."));
        }

    }
}
