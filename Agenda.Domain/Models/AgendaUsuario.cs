using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Helpers;
using Agenda.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Models
{
    public class AgendaUsuario : Entity, IAggregateRoot
    {
        public Guid AgendaId { get; protected set; }
        public Guid UsuarioId { get; protected set; }

        public AgendaUsuario(Guid agendaId, Guid usuarioId)
        {
            AgendaId = agendaId;
            UsuarioId = usuarioId;
        }

        public void DefinirUsuarioId(Guid usuarioId)
        {
            if (usuarioId.EhVazio())
            {
                throw new DomainException("Por favor, certifique-se que adicinou uma pessoa.");
            }

            UsuarioId = usuarioId;
        }


        public void DefinirAgendaId(Guid agendaId)
        {
            if (AgendaId.EhVazio())
            {
                throw new DomainException("Por favor, certifique-se que adicinou uma agenda.");
            }

            AgendaId = agendaId;
        }

        public ValidationResult NovaAgendaUsuarioEhValido()
        {
            return new AgendaUsuarioValidation().Validate(this);
        }
    }
}
