
using Schedule.io.Core.Validations.AgendaValidations;

namespace Schedule.io.Core.Commands.Agenda
{
    public class AtualizarAgendaCommand : AgendaCommand
    {
        public AtualizarAgendaCommand(string id, string titulo, string descricao, bool publico)
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
