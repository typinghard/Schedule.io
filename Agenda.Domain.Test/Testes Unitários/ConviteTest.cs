using Agenda.Domain.Models;
using Bogus;
using Xunit;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Test
{
    public class ConviteTest
    {
        private Convite eventoUsuario;
        private PermissoesConvite permissao;

        public ConviteTest()
        {
            permissao = new Faker<PermissoesConvite>("pt_BR")
                .CustomInstantiator((p) => new PermissoesConvite(p.Random.Bool(), p.Random.Bool(), p.Random.Bool()))
                .Generate(1)
                .First();

            eventoUsuario = new Faker<Convite>("pt_BR")
                .CustomInstantiator((f) => new Convite(f.Random.Guid(), f.Random.Bool(), permissao))
                .Generate(1)
                .First();
        }


        [Fact(DisplayName = "EventoUsuario - DefinirUsuarioId - UsuarioId deve ser alterado.")]
        public void EventoUsuario_DefinirUsuarioId_UsuarioIdDeveSerAlterado()
        {
            //Arrange
            var novoUsuarioId = Guid.NewGuid();

            //Act
            eventoUsuario.DefinirUsuarioId(novoUsuarioId);

            //Assert
            Assert.Equal(novoUsuarioId, eventoUsuario.UsuarioId);
        }

        [Fact(DisplayName = "EventoUsuario - ConfirmacaoUsuario - Confirmação Usuário deve ser alterado.")]
        public void EventoUsuario_ConfirmacaoUsuario_ConfirmacaoUsuarioDeveSerAlterado()
        {
            //Arrange
            var confirmacao = true;

            //Act
            eventoUsuario.ConfirmacaoUsuario();

            //Assert
            Assert.Equal(confirmacao, eventoUsuario.Status);
        }

        [Fact(DisplayName = "EventoUsuario - RemoverConfirmacaoUsuario - Remover Confirmação Usuário deve ser alterado.")]
        public void EventoUsuario_RemoverConfirmacaoUsuario_RemoverConfirmacaoUsuarioDeveSerAlterado()
        {
            //Arrange
            var removerConfirmacao = false;

            //Act
            eventoUsuario.RemoverConfirmacaoUsuario();

            //Assert
            Assert.Equal(removerConfirmacao, eventoUsuario.Status);
        }

        [Fact(DisplayName = "EventoUsuario - Permissao - PodeModificarEvento - Pode Modificar Evento deve ser alterado.")]
        public void EventoUsuario_Permissao_PodeModificarEvento_PodeModificarEventoDeveSerAlterado()
        {
            //Arrange
            var podeModificar = true;

            //Act
            eventoUsuario.Permissoes.PodeModificarEvento();

            //Assert
            Assert.Equal(podeModificar, eventoUsuario.Permissoes.ModificaEvento);
        }

        [Fact(DisplayName = "EventoUsuario - Permissao - NaoPodeModificarEvento - Não Pode Modificar Evento deve ser alterado.")]
        public void EventoUsuario_Permissao_NaoPodeModificarEventoo_NaoPodeModificarDeveSerAlterado()
        {
            //Arrange
            var podeModificar = false;

            //Act
            eventoUsuario.Permissoes.NaoPodeModificarEvento();

            //Assert
            Assert.Equal(podeModificar, eventoUsuario.Permissoes.ModificaEvento);
        }

        [Fact(DisplayName = "EventoUsuario - Permissao - PodeConvidar - Pode Convidar deve ser alterado.")]
        public void EventoUsuario_Permissao_PodeConvidar_PodeConvidarEventoDeveSerAlterado()
        {
            //Arrange
            var podeConvidar = true;

            //Act
            eventoUsuario.Permissoes.PodeConvidar();

            //Assert
            Assert.Equal(podeConvidar, eventoUsuario.Permissoes.ConvidaUsuario);
        }

        [Fact(DisplayName = "EventoUsuario - Permissao - NaoPodeConvidar - Não Pode Convidar deve ser alterado.")]
        public void EventoUsuario_Permissao_NaoPodeConvidar_NaoPodeConvidarEventoDeveSerAlterado()
        {
            //Arrange
            var podeConvidar = false;

            //Act
            eventoUsuario.Permissoes.NaoPodeConvidar();

            //Assert
            Assert.Equal(podeConvidar, eventoUsuario.Permissoes.ConvidaUsuario);
        }


        [Fact(DisplayName = "EventoUsuario - Permissao - PodeVerListaDeConvidados - Pode Ver Lista Convidados deve ser alterado.")]
        public void EventoUsuario_Permissao_PodeVerListaDeConvidados_PodeVerListaConvidadosDeveSerAlterado()
        {
            //Arrange
            var podeVerLista = true;

            //Act
            eventoUsuario.Permissoes.PodeVerListaDeConvidados();

            //Assert
            Assert.Equal(podeVerLista, eventoUsuario.Permissoes.VeListaDeConvidados);
        }

        [Fact(DisplayName = "EventoUsuario - Permissao - NaoPodeVerListaDeConvidados - Não Pode Ver Lista Convidados deve ser alterado.")]
        public void EventoUsuario_Permissao_NaoPodeVerListaDeConvidados_NaoPodeVerListaConvidadosDeveSerAlterado()
        {
            //Arrange
            var podeVerLista = false;

            //Act
            eventoUsuario.Permissoes.NaoPodeVerListaDeConvidados();

            //Assert
            Assert.Equal(podeVerLista, eventoUsuario.Permissoes.VeListaDeConvidados);
        }

        [Fact(DisplayName = "EventoUsuario - NovoEventoUsuarioEhValido - Deve Ser Valido")]
        public void EventoUsuario_NovoEventoUsuarioEhValido_DeveSerValido()
        {
            //Act
            var ehValido = eventoUsuario.NovoConviteEhValido().IsValid;

            //Assert
            Assert.True(ehValido);
        }

        [Fact(DisplayName = "EventoUsuario - NovoEventoUsuarioEhValido - Deve Ser Inválido")]
        public void EventoUsuario_NovoEventoUsuarioEhValido_DeveSerInvalido()
        {
            //Arrange
            eventoUsuario = new Faker<Convite>("pt_BR")
                .CustomInstantiator((f) => new Convite(Guid.Empty, f.Random.Bool(), permissao))
                .Generate(1)
                .First();

            //Act
            var ehValido = eventoUsuario.NovoConviteEhValido().IsValid;

            //Assert
            Assert.False(ehValido);
        }
    }
}
