using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class RegistrarAgendaCommand : AgendaCommand
    {
        public RegistrarAgendaCommand(string titulo, string descricao, bool publico)
        {
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
