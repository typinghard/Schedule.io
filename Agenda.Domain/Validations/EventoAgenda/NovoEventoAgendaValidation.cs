using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Validations
{
    public class NovoEventoAgendaValidation : EntityValidation<EventoAgenda>
    {
        public NovoEventoAgendaValidation()
        {
            RuleFor(c => c.AgendaId)
                .NotEmpty()
                .WithMessage("Id da Agenda não pode ser vazio!");

            RuleFor(e => e.Titulo)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, certifique-se que digitou um título.")
                .Length(2, 150).WithMessage("O título deve ter entre 2 e 150 caracteres.");

            RuleFor(e => e.DataInicio)
                .NotEmpty()
                .NotEqual(DateTime.MinValue)
                .WithMessage("Por favor, escolha a data e hora inicial do evento.");

            RuleFor(e => e.Frequencia)
                .NotNull()
                .WithMessage("Frequencia não pode ser nula!");

            RuleFor(e => e.Tipo.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, certifique-se que digitou um Nome para o Tipo do Evento.")
                .Length(2, 120).WithMessage("O Nome do Tipo do Evento deve ter entre 2 e 120 caracteres.");

            RuleFor(e => e.Tipo.Descricao)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, certifique-se que digitou uma Descrição para o Tipo do Evento.")
                .Length(2, 500).WithMessage("A Descrição do Tipo do Evento deve ter entre 2 e 500 caracteres.");

            RuleFor(e => e.QuantidadeMinimaDeUsuarios)
              .Must(QuantidadeMinimaDeUsuario)
              .WithMessage("Por favor, certifique-se qua a quantidade mínima de usuários para o evento não é menor que 0.");

            RuleFor(e => e.OcupaUsuario)
                .NotNull()
                .WithMessage("Ocupar Usuário não pode ser nulo");

            RuleFor(e => e.Publico)
                .NotNull()
                .WithMessage("Evento Público/Privado não pode ser nulo");
        }

        protected static bool QuantidadeMinimaDeUsuario(int qtdeUsuario)
        {
            return qtdeUsuario < 0 ? false : true;
        }

        protected static bool ValidaPessoas(IList<string> pessoas)
        {
            if (pessoas != null && pessoas.Count > 0)
                foreach (var pessoaId in pessoas)
                    if (pessoaId.Equals(Guid.Empty))
                        return false;


            return true;
        }

    }
}
