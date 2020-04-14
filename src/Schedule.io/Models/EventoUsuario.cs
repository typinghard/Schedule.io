using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Models
{
    public class EventoUsuario : Entity, IAggregateRoot
    {
        public string UsuarioId { get; private set; }
        public bool Confirmacao { get; private set; }

        public Permissao Permissao { get; private set; }

        public EventoUsuario(string usuarioId, bool confirmacao, Permissao permissao)
        {
            UsuarioId = usuarioId;
            Confirmacao = confirmacao;
            Permissao = permissao;
        }

        public void DefinirStatusConfirmacaoDoUsuario(bool confirmacao)
        {
            if (confirmacao)
                ConfirmacaoUsuario();
            else
                RemoverConfirmacaoUsuario();
        }

        private void ConfirmacaoUsuario()
        {
            Confirmacao = true;
        }

        private void RemoverConfirmacaoUsuario()
        {
            Confirmacao = false;
        }

        public void DefinirUsuarioId(string usuarioId)
        {
            if (usuarioId.EhVazio())
            {
                throw new ScheduleIoException("Por favor, certifique-se que adicinou uma pessoa.");
            }

            UsuarioId = usuarioId;
        }

        public void DefinirPermissao(Permissao permissao)
        {
            permissao.DefinirSePodeModificarEvento(permissao.ModificaEvento);
            permissao.DefinirSePodeConvidar(permissao.ConvidaUsuario);
            permissao.DefinirSePodeVerListaDeConvidados(permissao.VeListaDeConvidados);

            this.Permissao = permissao;
            ///instancia uma nova tipo evento antes de atribuir? 
        }
    }

    public class Permissao
    {
        public bool ModificaEvento { get; private set; }
        public bool ConvidaUsuario { get; private set; }
        public bool VeListaDeConvidados { get; private set; }

        /*
             Modificar Evento
             Convidar outros
             Ver Lista de convidados
        */

        public Permissao(bool modificaEvento, bool convidaUsuario, bool veListaDeConvidados)
        {
            ModificaEvento = modificaEvento;
            ConvidaUsuario = convidaUsuario;
            VeListaDeConvidados = veListaDeConvidados;
        }

        public void DefinirSePodeModificarEvento(bool podeModificar)
        {
            if (podeModificar)
                this.PodeModificarEvento();
            else
                this.NaoPodeModificarEvento();
        }
        private void PodeModificarEvento()
        {
            ModificaEvento = true;
        }

        private void NaoPodeModificarEvento()
        {
            ModificaEvento = false;
        }


        public void DefinirSePodeConvidar(bool podeConvidar)
        {
            if (podeConvidar)
                this.PodeConvidar();
            else
                this.NaoPodeConvidar();
        }

        private void PodeConvidar()
        {
            ConvidaUsuario = true;
        }

        private void NaoPodeConvidar()
        {
            ConvidaUsuario = false;
        }


        public void DefinirSePodeVerListaDeConvidados(bool podeVerListaConvidados)
        {
            if (podeVerListaConvidados)
                this.PodeVerListaDeConvidados();
            else
                NaoPodeVerListaDeConvidados();
        }

        private void PodeVerListaDeConvidados()
        {
            VeListaDeConvidados = true;
        }

        private void NaoPodeVerListaDeConvidados()
        {
            VeListaDeConvidados = false;
        }

    }
}
