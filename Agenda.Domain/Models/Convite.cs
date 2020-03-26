using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Helpers;
using Agenda.Domain.Enums;
using Agenda.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agenda.Domain.Models
{
    public class Convite : Entity, IAggregateRoot
    {
        public string EventoId { get; private set; }
        public string UsuarioId { get; private set; }
        public EnumStatusConviteEvento Status { get; private set; }
        public PermissoesConvite Permissoes { get; private set; }

        public Convite(string eventoId, string usuarioId)
        {
            EventoId = eventoId;
            UsuarioId = usuarioId;
            Status = EnumStatusConviteEvento.Aguardando_Confirmacao;
            Permissoes = new PermissoesConvite();

            var resultadoValidacao = this.NovoConviteEhValido();
            if (!resultadoValidacao.IsValid)
                throw new DomainException(string.Join(", ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        public void DefinirUsuarioId(string usuarioId)
        {
            if (usuarioId.EhVazio())
            {
                throw new DomainException("Por favor, certifique-se que adicinou uma pessoa.");
            }

            UsuarioId = usuarioId;
        }

        public void DefinirEventoId(string eventoId)
        {
            if (eventoId.EhVazio())
            {
                throw new DomainException("Por favor, certifique-se que adicinou um evento.");
            }

            EventoId = eventoId;
        }

        public void AtualizarStatusConvite(EnumStatusConviteEvento status)
        {
            Status = status;
        }

        public ValidationResult NovoConviteEhValido()
        {
            return new NovoConviteValidation().Validate(this);
        }
    }

    public class PermissoesConvite
    {
        public bool ModificaEvento { get; private set; } 
        public bool ConvidaUsuario { get; private set; } 
        public bool VeListaDeConvidados { get; private set; } 

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
