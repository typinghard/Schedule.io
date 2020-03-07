using Agenda.Domain.Validations;
using System;

namespace Agenda.Domain.Commands
{
    public class AtualizarAgendaCommand : AgendaCommand
    {
        public AtualizarAgendaCommand(Guid id, string titulo, string descricao, bool publico)
        {
            this.Id = id;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Publico = publico;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarAgendaCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
