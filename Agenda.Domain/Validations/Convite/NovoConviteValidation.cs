using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Validations
{
    public class NovoConviteValidation : EntityValidation<Convite>
    {
        public NovoConviteValidation()
        {
            RuleFor(e => e.EventoId)
                .NotEmpty()
                .WithMessage("EventoId não informado!");

            RuleFor(e => e.UsuarioId)
                .NotEmpty()
                .WithMessage("UsuarioId não informado!");

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
