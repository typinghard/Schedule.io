using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
   public class AtualizarAgendaUsuarioCommand : AgendaUsuarioCommand
    {
        public AtualizarAgendaUsuarioCommand(string id, string agendaId, string usuarioId)
        {
            this.Id = id;
            this.AggregateId = id;
            this.AgendaId = agendaId;
            this.UsuarioId = usuarioId;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarAgendaUsuarioCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
