using FluentValidation.Results;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Helpers;
using Schedule.io.Enums;
using Schedule.io.Validations.AgendaValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Models.ValueObjects
{
    public class Convite 
    {
        public string EventoId { get; private set; }
        public string UsuarioId { get; private set; }
        public string EmailConvidado { get; private set; }
        public EnumStatusConviteEvento Status { get; private set; }
        public PermissoesConvite Permissoes { get; private set; }

        public Convite(string id, string eventoId, string usuarioId, string emailConvidado) 
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
}
