using Agenda.Domain.Enums;
using Agenda.Domain.Models;
using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class AtualizarConviteCommand : ConviteCommand
    {
        public AtualizarConviteCommand(string id, string eventoId, string usuarioId, string emailConvidado, EnumStatusConviteEvento status, PermissoesConvite permissoes)
        {
            Id = id;
            EventoId = eventoId;
            UsuarioId = usuarioId;
            EmailConvidado = emailConvidado;
            Status = status;
            Permissoes = permissoes;
        }


        public override bool EhValido()
        {
            ValidationResult = new AtualizarConviteCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
