using Agenda.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Validations
{
    public abstract class EventoAgendaValidacao<T> : AbstractValidator<T> where T : EventoAgendaCommand
    {
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

        //protected void ValidateBirthDate()
        //{
        //    RuleFor(c => c.BirthDate)
        //        .NotEmpty()
        //        .Must(HaveMinimumAge)
        //        .WithMessage("The customer must have 18 years or more");
        //}

        //protected void ValidateQuantidadeDeUsuariosReferenteALotacaoMaximadoLocal()
        //{
        //    RuleFor(c => c.Pessoas.Count).GreaterThan(c => c.Local.LotacaoMaxima)
        //        .WithMessage("A quantidade de pessoas excede a lotação máxima do local.");
        //}

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}
