using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Models;
using FluentValidation;

namespace Agenda.Domain.Validations
{
    public class NovoEventoAgendaValidation : EntityValidation<EventoAgenda>, EventoAgendaValidacao<EventoAgenda>
    {
        public NovoEventoAgendaValidation()
        {
            ValidaTitulo();
            ValidaDescricao();
            ValidaDataInicialEvento();
            ValidaTipoEvento();
        }

        protected void ValidaTitulo()
        {
            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("Por favor, certifique-se que digitou um título.")
                .Length(2, 150).WithMessage("O título deve ter entre 2 e 150 caracteres.");
        }

        protected void ValidaDescricao()
        {
            RuleFor(c => c.Descricao)
                .Length(2, 500).WithMessage("A descrição deve ter entre 2 e 500 caracteres.");
        }

        protected void ValidaDataInicialEvento()
        {
            RuleFor(c => c.DataInicio)
                .NotNull().WithMessage("Por favor, escolha a data e hora inicial do evento.");
        }

        protected void ValidaTipoEvento()
        {
            RuleFor(c => c.TipoEvento)
                .NotNull().WithMessage("Por favor, cetifique-se de que escolher o Tipo do Evento.");
        }
    }
}
