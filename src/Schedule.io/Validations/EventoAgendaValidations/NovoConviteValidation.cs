using FluentValidation;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Validations.AgendaValidations
{
    public class NovoConviteValidation : AbstractValidator<Convite>
    {
        public NovoConviteValidation()
        {
            RuleFor(e => e.EventoId)
                .NotEmpty()
                .WithMessage("{PropertyName} não informado!");

            RuleFor(e => e.UsuarioId)
                .NotEmpty()
                .WithMessage("{PropertyName} não informado!");

            RuleFor(e => e.Status)
                .NotNull()
                .WithMessage("Confirmação não pode ser nulo!");

            RuleFor(e => e.Permissoes.ModificaEvento)
                .NotNull()
                .WithMessage("Permissão Mdificar Evento não pode ser nulo!");

            RuleFor(e => e.Permissoes.ConvidaUsuario)
                .NotNull()
                .WithMessage("Permissão Convida Usuário não pode ser nulo!");

            RuleFor(e => e.Permissoes.VeListaDeConvidados)
                .NotNull()
                .WithMessage("Permissão Ver Lista de Convidados não pode ser nulo!");
        }
    }
}
