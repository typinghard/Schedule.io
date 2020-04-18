using FluentValidation.Results;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Helpers;
using Schedule.io.Enums;
using Schedule.io.Validations.AgendaValidations;
using System.Linq;

namespace Schedule.io.Models.ValueObjects
{
    public class Convite
    {
        public string UsuarioId { get; private set; }
        public string EventoId { get; private set; }
        public EnumStatusConviteEvento Status { get; private set; }
        public PermissoesConvite Permissoes { get; private set; }

        public Convite(string eventoId, string usuarioId)
        {
            EventoId = eventoId;
            UsuarioId = usuarioId;
            Status = EnumStatusConviteEvento.Aguardando_Confirmacao;
            Permissoes = new PermissoesConvite();

            var resultadoValidacao = this.NovoConviteEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(string.Join("## ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        private Convite()
        {
            Permissoes = new PermissoesConvite();
        }

        public void DefinirUsuarioId(string usuarioId)
        {
            if (usuarioId.EhVazio())
                throw new ScheduleIoException("Por favor, certifique-se que adicinou uma pessoa.");

            UsuarioId = usuarioId;
        }

        public void DefinirEventoId(string eventoId)
        {
            if (eventoId.EhVazio())
                throw new ScheduleIoException("Por favor, certifique-se que adicinou um evento.");

            EventoId = eventoId;
        }

        public void AtualizarStatusConvite(EnumStatusConviteEvento status)
        {
            Status = status;
        }

        public bool ConviteEhValido()
        {
            return NovoConviteEhValido().IsValid;
        }

        private ValidationResult NovoConviteEhValido()
        {
            return new ConviteValidation().Validate(this);
        }
    }
}
