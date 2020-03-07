using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class RegistrarAgendaUsuarioCommand : AgendaUsuarioCommand
    {
        public RegistrarAgendaUsuarioCommand(Guid agendaId, Guid usuarioId)
        {
            this.AgendaId = agendaId;
            this.UsuarioId = usuarioId;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarAgendaUsuarioCommandValidacao().Validate(this);
            return ValidationResult.IsValid;

        }
    }
}
