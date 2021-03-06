﻿using FluentValidation;
using Schedule.io.Models.ValueObjects;

namespace Schedule.io.Validations.AgendaValidations
{
    public class ConviteValidation : AbstractValidator<Convite>
    {
        public ConviteValidation()
        {
            RuleFor(e => e.UsuarioId)
                .NotEmpty()
                .WithMessage("UsuarioId não informado!");

            RuleFor(e => e.Status)
                .NotNull()
                .WithMessage("Confirmação não pode ser nulo!");

            RuleFor(e => e.Permissoes.ModificaEvento)
                .NotNull()
                .WithMessage("Permissão Modificar Evento não pode ser nulo!");

            RuleFor(e => e.Permissoes.ConvidaUsuario)
                .NotNull()
                .WithMessage("Permissão Convida Usuário não pode ser nulo!");

            RuleFor(e => e.Permissoes.VeListaDeConvidados)
                .NotNull()
                .WithMessage("Permissão Ver Lista de Convidados não pode ser nulo!");
        }
    }
}
