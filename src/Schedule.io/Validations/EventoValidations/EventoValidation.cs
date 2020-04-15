using FluentValidation;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Models.AggregatesRoots;
using System;

namespace Schedule.io.Validations.EventoAgendaValidations
{
    public class EventoValidation : EntityValidation<Evento>
    {
        public EventoValidation()
        {
            RuleFor(c => c.AgendaId)
                .NotEmpty()
                .WithMessage("Id da Agenda não informado!");

            RuleFor(c => c.UsuarioIdCriador)
                .NotEmpty()
                .WithMessage("Id do Usuario dono da agenda não informado!");

            RuleFor(e => e.Titulo)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, certifique-se que digitou um {PropertyName}.")
                .Length(2, 150).WithMessage("O título deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(e => e.DataInicio)
                .NotEmpty()
                .NotEqual(DateTime.MinValue)
                .WithMessage("Por favor, escolha a data e hora inicial do evento.");

            RuleFor(e => e.Frequencia)
                .NotNull()
                .WithMessage("{PropertyName} não pode ser nula!");

            RuleFor(e => e.QuantidadeMinimaDeUsuarios)
              .GreaterThanOrEqualTo(0)
              .WithMessage("Por favor, certifique-se qua a quantidade mínima de usuários para o evento não é menor que 0.");

            RuleFor(e => e.OcupaUsuario)
                .NotNull()
                .WithMessage("Ocupar Usuário não pode ser nulo");

            RuleFor(e => e.Publico)
                .NotNull()
                .WithMessage("Evento Público/Privado não pode ser nulo");
        }
    }
}
