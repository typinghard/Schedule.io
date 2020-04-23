using Bogus;
using Xunit;
using System.Linq;
using System;
using Schedule.io.Models.ValueObjects;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Enums;

namespace Schedule.io.Test.Testes_Unitários.Models.ValueObjects
{
    public class ConviteTests
    {
        private Convite convite;

        public ConviteTests()
        {
            convite = new Faker<Convite>("pt_BR")
                .CustomInstantiator((f) => new Convite(f.Random.Guid().ToString()))
                .Generate(1)
                .First();
        }

        [Fact(DisplayName = "Convite - DefinirEventoId - EventoId deve ser alterado.")]
        public void Convite_DefinirEventoId_EventoIdDeveSerAlterado()
        {
            //Arrange
            var novoEventoId = Guid.NewGuid().ToString();

            //Act
            convite.AssociarEvento(novoEventoId);

            //Assert
            Assert.Equal(novoEventoId, convite.EventoId);
        }

        [Fact(DisplayName = "Convite - DefinirEventoId - EventoId deve ser inválido por ser vazio.")]
        public void Convite_DefinirEventoId_EventoIdDeveSerInvalidoPorSerVazio()
        {
            //Arrange
            var novoEventoId = string.Empty;

            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => convite.AssociarEvento(novoEventoId)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("Por favor, certifique-se que adicinou um evento."));
        }


        [Fact(DisplayName = "Convite - DefinirUsuarioId - UsuarioId deve ser alterado.")]
        public void Convite_DefinirUsuarioId_UsuarioIdDeveSerAlterado()
        {
            //Arrange
            var novoUsuarioId = Guid.NewGuid().ToString();

            //Act
            convite.DefinirUsuarioId(novoUsuarioId);

            //Assert
            Assert.Equal(novoUsuarioId, convite.UsuarioId);
        }

        [Fact(DisplayName = "Convite - DefinirUsuarioId - UsuarioId deve ser inválido por ser vazio.")]
        public void Convite_DefinirUsuarioId_UsuarioIdDeveSerInvalidoPorSerVazio()
        {
            //Arrange
            var novoUsuarioId = string.Empty;

            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => convite.DefinirUsuarioId(novoUsuarioId)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("Por favor, certifique-se que adicinou uma pessoa."));
        }


        [Fact(DisplayName = "Convite - AtualizarStatusConvite - AtualizarStatusConvite deve ser alterado.")]
        public void Convite_AtualizarStatusConvite_AtualizarStatusConviteDeveSerAlterado()
        {
            //Arrange
            var novoStatus = new Faker().Random.Enum<EnumStatusConviteEvento>();

            //Act
            convite.AtualizarStatusConvite(novoStatus);

            //Assert
            Assert.Equal(novoStatus, convite.Status);
        }


        [Fact(DisplayName = "Convite - Permissões - PodeModificarEvento - PodeModificarEvento deve ser alterado.")]
        public void Convite_Permissoes_PodeModificarEvento_PodeModificarEventoDeveSerAlterado()
        {
            //Arrange
            var confirmacao = true;

            //Act
            convite.Permissoes.PodeModificarEvento();

            //Assert
            Assert.Equal(confirmacao, convite.Permissoes.ModificaEvento);
        }

        [Fact(DisplayName = "Convite - Permissões - NaoPodeModificarEvento - NaoPodeModificarEvento deve ser alterado.")]
        public void Convite_Permissoes_NaoPodeModificarEventoo_NaoPodeModificarEventoDeveSerAlterado()
        {
            //Arrange
            var confirmacao = false;

            //Act
            convite.Permissoes.NaoPodeModificarEvento();

            //Assert
            Assert.Equal(confirmacao, convite.Permissoes.ModificaEvento);
        }


        [Fact(DisplayName = "Convite - Permissões - PodeVerListaDeConvidados - PodeVerListaDeConvidados deve ser alterado.")]
        public void Convite_Permissoes_PodeVerListaDeConvidados_PodeVerListaDeConvidadosDeveSerAlterado()
        {
            //Arrange
            var confirmacao = true;

            //Act
            convite.Permissoes.PodeVerListaDeConvidados();

            //Assert
            Assert.Equal(confirmacao, convite.Permissoes.VeListaDeConvidados);
        }

        [Fact(DisplayName = "Convite - Permissões - NaoPodeVerListaDeConvidados - NaoPodeVerListaDeConvidados deve ser alterado.")]
        public void Convite_Permissoes_NaoPodeVerListaDeConvidados_NaoPodeVerListaDeConvidadosDeveSerAlterado()
        {
            //Arrange
            var confirmacao = false;

            //Act
            convite.Permissoes.NaoPodeVerListaDeConvidados();

            //Assert
            Assert.Equal(confirmacao, convite.Permissoes.VeListaDeConvidados);
        }


        [Fact(DisplayName = "Convite - Permissões - PodeConvidar - PodeConvidar deve ser alterado.")]
        public void Convite_Permissoes_PodeConvidar_PodeConvidarDeveSerAlterado()
        {
            //Arrange
            var confirmacao = true;

            //Act
            convite.Permissoes.PodeConvidar();

            //Assert
            Assert.Equal(confirmacao, convite.Permissoes.ConvidaUsuario);
        }

        [Fact(DisplayName = "Convite - Permissões - NaoPodeConvidar - NaoPodeConvidar deve ser alterado.")]
        public void Convite_Permissoes_NaoPodeConvidar_NaoPodeConvidarDeveSerAlterado()
        {
            //Arrange
            var confirmacao = false;

            //Act
            convite.Permissoes.NaoPodeConvidar();

            //Assert
            Assert.Equal(confirmacao, convite.Permissoes.ConvidaUsuario);
        }


        [Fact(DisplayName = "Convite - NovoConviteEhValido - Deve Ser Valido")]
        public void Convite_NovoConviteEhValido_DeveSerValido()
        {
            //Act
            var ehValido = convite.ConviteEhValido();

            //Assert
            Assert.True(ehValido);
        }

        [Fact(DisplayName = "Convite - NovoConviteEhValido - Deve Ser Inválido")]
        public void Convite_NovoConviteEhValido_DeveSerInvalido()
        {
            //Arrange
            var validacao = Assert.Throws<ScheduleIoException>(() => new Faker<Convite>("pt_BR")
                                                                    .CustomInstantiator((f) => new Convite(""))
                                                                    .Generate(1)
                                                                    .First()).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("UsuarioId não informado!"));
        }
    }
}
