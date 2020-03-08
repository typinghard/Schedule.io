﻿using Agenda.Domain.Models;
using Bogus;
using Xunit;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Test
{
    public class EventoUsuarioTest
    {
        private EventoUsuario eventoUsuario;
        private Permissao permissao;

        public EventoUsuarioTest()
        {
            permissao = new Faker<Permissao>("pt_BR")
                .CustomInstantiator((p) => new Permissao(p.Random.Bool(), p.Random.Bool(), p.Random.Bool()))
                .Generate(1)
                .First();

            eventoUsuario = new Faker<EventoUsuario>("pt_BR")
                .CustomInstantiator((f) => new EventoUsuario(f.Random.Guid(), f.Random.Bool(), permissao))
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
            Assert.Equal(confirmacao, eventoUsuario.Confirmacao);
        }

        [Fact(DisplayName = "EventoUsuario - RemoverConfirmacaoUsuario - Remover Confirmação Usuário deve ser alterado.")]
        public void EventoUsuario_RemoverConfirmacaoUsuario_RemoverConfirmacaoUsuarioDeveSerAlterado()
        {
            //Arrange
            var removerConfirmacao = false;

            //Act
            eventoUsuario.RemoverConfirmacaoUsuario();

            //Assert
            Assert.Equal(removerConfirmacao, eventoUsuario.Confirmacao);
        }

        [Fact(DisplayName = "EventoUsuario - Permissao - PodeModificarEvento - Pode Modificar Evento deve ser alterado.")]
        public void EventoUsuario_Permissao_PodeModificarEvento_PodeModificarEventoDeveSerAlterado()
        {
            //Arrange
            var podeModificar = true;

            //Act
            eventoUsuario.Permissao.PodeModificarEvento();

            //Assert
            Assert.Equal(podeModificar, eventoUsuario.Permissao.ModificaEvento);
        }

        [Fact(DisplayName = "EventoUsuario - Permissao - NaoPodeModificarEvento - Não Pode Modificar Evento deve ser alterado.")]
        public void EventoUsuario_Permissao_NaoPodeModificarEventoo_NaoPodeModificarDeveSerAlterado()
        {
            //Arrange
            var podeModificar = false;

            //Act
            eventoUsuario.Permissao.NaoPodeModificarEvento();

            //Assert
            Assert.Equal(podeModificar, eventoUsuario.Permissao.ModificaEvento);
        }

        [Fact(DisplayName = "EventoUsuario - Permissao - PodeConvidar - Pode Convidar deve ser alterado.")]
        public void EventoUsuario_Permissao_PodeConvidar_PodeConvidarEventoDeveSerAlterado()
        {
            //Arrange
            var podeConvidar = true;

            //Act
            eventoUsuario.Permissao.PodeConvidar();

            //Assert
            Assert.Equal(podeConvidar, eventoUsuario.Permissao.ConvidaUsuario);
        }

        [Fact(DisplayName = "EventoUsuario - Permissao - NaoPodeConvidar - Não Pode Convidar deve ser alterado.")]
        public void EventoUsuario_Permissao_NaoPodeConvidar_NaoPodeConvidarEventoDeveSerAlterado()
        {
            //Arrange
            var podeConvidar = false;

            //Act
            eventoUsuario.Permissao.NaoPodeConvidar();

            //Assert
            Assert.Equal(podeConvidar, eventoUsuario.Permissao.ConvidaUsuario);
        }


        [Fact(DisplayName = "EventoUsuario - Permissao - PodeVerListaDeConvidados - Pode Ver Lista Convidados deve ser alterado.")]
        public void EventoUsuario_Permissao_PodeVerListaDeConvidados_PodeVerListaConvidadosDeveSerAlterado()
        {
            //Arrange
            var podeVerLista = true;

            //Act
            eventoUsuario.Permissao.PodeVerListaDeConvidados();

            //Assert
            Assert.Equal(podeVerLista, eventoUsuario.Permissao.VeListaDeConvidados);
        }

        [Fact(DisplayName = "EventoUsuario - Permissao - NaoPodeVerListaDeConvidados - Não Pode Ver Lista Convidados deve ser alterado.")]
        public void EventoUsuario_Permissao_NaoPodeVerListaDeConvidados_NaoPodeVerListaConvidadosDeveSerAlterado()
        {
            //Arrange
            var podeVerLista = false;

            //Act
            eventoUsuario.Permissao.NaoPodeVerListaDeConvidados();

            //Assert
            Assert.Equal(podeVerLista, eventoUsuario.Permissao.VeListaDeConvidados);
        }

        [Fact(DisplayName = "EventoUsuario - NovoEventoUsuarioEhValido - Deve Ser Valido")]
        public void EventoUsuario_NovoEventoUsuarioEhValido_DeveSerValido()
        {
            //Act
            var ehValido = eventoUsuario.NovoEventoUsuarioEhValido().IsValid;

            //Assert
            Assert.True(ehValido);
        }

        [Fact(DisplayName = "EventoUsuario - NovoEventoUsuarioEhValido - Deve Ser Inválido")]
        public void EventoUsuario_NovoEventoUsuarioEhValido_DeveSerInvalido()
        {
            //Arrange
            eventoUsuario = new Faker<EventoUsuario>("pt_BR")
                .CustomInstantiator((f) => new EventoUsuario(Guid.Empty, f.Random.Bool(), permissao))
                .Generate(1)
                .First();

            //Act
            var ehValido = eventoUsuario.NovoEventoUsuarioEhValido().IsValid;

            //Assert
            Assert.False(ehValido);
        }
    }
}
