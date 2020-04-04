using Schedule.io.Core.Validations.AgendaUsuarioValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.AgendaUsuarioCommands
{
    public class RegistrarAgendaUsuarioCommand : AgendaUsuarioCommand
    {
        public RegistrarAgendaUsuarioCommand(string agendaId, string usuarioId)
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
