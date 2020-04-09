using FluentValidation.Results;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Core.Helpers;
using Schedule.io.Core.Enums;
using Schedule.io.Core.Validations.ConviteValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Core.Models
{
    public class Convite : Entity, IAggregateRoot
    {
        public string EventoId { get; private set; }
        public string UsuarioId { get; private set; }
        public string EmailConvidado { get; private set; }
        public EnumStatusConviteEvento Status { get; private set; }
        public PermissoesConvite Permissoes { get; private set; }

        public Convite(string id, string eventoId, string usuarioId, string emailConvidado) : base(id)
        {
            EventoId = eventoId;
            UsuarioId = usuarioId;
            EmailConvidado = emailConvidado;
            Status = EnumStatusConviteEvento.Aguardando_Confirmacao;
            Permissoes = new PermissoesConvite();

            var resultadoValidacao = this.NovoConviteEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(string.Join(", ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        public void DefinirUsuarioId(string usuarioId)
        {
            if (usuarioId.EhVazio())
            {
                throw new ScheduleIoException("Por favor, certifique-se que adicinou uma pessoa.");
            }

            UsuarioId = usuarioId;
        }

        public void DefinirEmailConvidado(string emailUsuario)
        {
            if (emailUsuario.EhVazio())
            {
                throw new ScheduleIoException("Por favor, certifique-se que adicinou um email para o convidado.");
            }

            EmailConvidado = emailUsuario;
        }

        public void DefinirEventoId(string eventoId)
        {
            if (eventoId.EhVazio())
            {
                throw new ScheduleIoException("Por favor, certifique-se que adicinou um evento.");
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
        public bool ModificaEvento { get; set; }
        public bool ConvidaUsuario { get; set; }
        public bool VeListaDeConvidados { get; set; }


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
