using FluentValidation.Results;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Helpers;
using Schedule.io.Enums;
using Schedule.io.Validations.AgendaValidations;
using System.Linq;

namespace Schedule.io.Models.ValueObjects
{
    public class Convite : ValueObject<Convite>
    {
        public string UsuarioId { get; private set; }
        public string EventoId { get; private set; }
        public EnumStatusConviteEvento Status { get; private set; }
        public PermissoesConvite Permissoes { get; private set; }

        public Convite(string usuarioId)
        {
            UsuarioId = usuarioId;
            Status = EnumStatusConviteEvento.AGUARDANDO_CONFIRMACAO;
            Permissoes = new PermissoesConvite();

            var resultadoValidacao = NovoConviteEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(resultadoValidacao.Errors.Select(x => x.ErrorMessage).ToList());
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

        public void AssociarEvento(string eventoId)
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

        protected override bool EqualsCore(Convite other)
        {
            return other.UsuarioId == UsuarioId &&
                   other.Status == Status &&
                   other.EventoId == EventoId &&
                   other.Permissoes == Permissoes;
        }
    }
}
