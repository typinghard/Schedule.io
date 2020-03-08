using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Helpers;
using Agenda.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Models
{
    public class EventoUsuario : Entity, IAggregateRoot
    {
        public Guid UsuarioId { get; private set; }
        public bool Confirmacao { get; private set; }

        public Permissao Permissao { get; private set; }

        public EventoUsuario(Guid usuarioId, bool confirmacao, Permissao permissao)
        {
            UsuarioId = usuarioId;
            Confirmacao = confirmacao;
            Permissao = permissao;
        }

        public void DefinirUsuarioId(Guid usuarioId)
        {
            if (usuarioId.EhVazio())
            {
                throw new DomainException("Por favor, certifique-se que adicinou uma pessoa.");
            }

            UsuarioId = usuarioId;
        }

        public void ConfirmacaoUsuario()
        {
            Confirmacao = true;
        }

        public void RemoverConfirmacaoUsuario()
        {
            Confirmacao = false;
        }

        public void DefinirPermissao(Permissao permissao)
        {
            if (permissao.ModificaEvento)
                permissao.PodeModificarEvento();
            else
                permissao.NaoPodeModificarEvento();

            if (permissao.ConvidaUsuario)
                permissao.PodeConvidar();
            else
                permissao.NaoPodeConvidar();

            if (permissao.VeListaDeConvidados)
                permissao.PodeVerListaDeConvidados();
            else
                permissao.NaoPodeVerListaDeConvidados();

            this.Permissao = permissao;
        }

        public ValidationResult NovoEventoUsuarioEhValido()
        {
            return new NovoEventoUsuarioValidation().Validate(this);
        }
    }

    public class Permissao
    {
        public bool ModificaEvento { get; private set; }
        public bool ConvidaUsuario { get; private set; }
        public bool VeListaDeConvidados { get; private set; }

        public Permissao(bool modificaEvento, bool convidaUsuario, bool veListaDeConvidados)
        {
            ModificaEvento = modificaEvento;
            ConvidaUsuario = convidaUsuario;
            VeListaDeConvidados = veListaDeConvidados;
        }

        public void PodeModificarEvento()
        {
            ModificaEvento = true;
        }

        public void NaoPodeModificarEvento()
        {
            ModificaEvento = false;
        }


        public void PodeConvidar()
        {
            ConvidaUsuario = true;
        }

        public void NaoPodeConvidar()
        {
            ConvidaUsuario = false;
        }




        public void PodeVerListaDeConvidados()
        {
            VeListaDeConvidados = true;
        }

        public void NaoPodeVerListaDeConvidados()
        {
            VeListaDeConvidados = false;
        }

    }
}
