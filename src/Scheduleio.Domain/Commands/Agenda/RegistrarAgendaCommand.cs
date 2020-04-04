using Schedule.io.Core.Validations.AgendaValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.Agenda
{
    public class RegistrarAgendaCommand : AgendaCommand
    {
        public RegistrarAgendaCommand(string id, string titulo, string descricao, bool publico)
        {
            this.Id = id;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Publico = publico;
        }

        public override bool EhValido()
        {
            ValidationResult = new  RegistrarAgendaCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
