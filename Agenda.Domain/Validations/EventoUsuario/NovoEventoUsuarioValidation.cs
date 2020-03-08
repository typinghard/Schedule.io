using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Validations
{
    public class NovoEventoUsuarioValidation : EntityValidation<EventoUsuario>
    {
        public NovoEventoUsuarioValidation()
        {
            RuleFor(e => e.UsuarioId)
                .NotEqual(Guid.Empty)
                .WithMessage("UsuarioId não informado!");

            RuleFor(e => e.Confirmacao)
                .NotNull()
                .WithMessage("Confirmação não pode ser nulo!");

            RuleFor(e => e.Permissao.ModificaEvento)
                .NotNull()
                .WithMessage("Permissão Mdificar Evento não pode ser nulo!");

            RuleFor(e => e.Permissao.ConvidaUsuario)
                .NotNull()
                .WithMessage("Permissão Convida Usuário não pode ser nulo!");

            RuleFor(e => e.Permissao.VeListaDeConvidados)
                .NotNull()
                .WithMessage("Permissão Ver Lista de Convidados não pode ser nulo!");
        }
    }
}
