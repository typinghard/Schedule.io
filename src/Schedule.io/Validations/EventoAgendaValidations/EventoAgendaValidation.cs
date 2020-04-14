using FluentValidation;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Validations.EventoAgendaValidations
{
    public class EventoAgendaValidation : EntityValidation<Evento>
    {
        public EventoAgendaValidation()
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

            //RuleFor(e => e.IdTipoEvento)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Por favor, certifique-se que digitou um Nome para o Tipo do Evento.")
            //    .Length(2, 120).WithMessage("O Nome do Tipo do Evento deve ter entre 2 e 120 caracteres.");

            //RuleFor(e => e.Tipo.Descricao)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Por favor, certifique-se que digitou uma Descrição para o Tipo do Evento.")
            //    .Length(2, 500).WithMessage("A Descrição do Tipo do Evento deve ter entre 2 e 500 caracteres.");

            RuleFor(e => e.QuantidadeMinimaDeUsuarios)
              .GreaterThan(0)
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
