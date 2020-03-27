using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Helpers;
using Agenda.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agenda.Domain.Models
{
    public class AgendaUsuario : Entity, IAggregateRoot
    {
        public string AgendaId { get; protected set; }
        public string UsuarioId { get; protected set; }
        public PermissoesAgenda Permissoes { get; protected set; }

        public AgendaUsuario(string agendaId, string usuarioId)
        {
            AgendaId = agendaId;
            UsuarioId = usuarioId;
            Permissoes = new PermissoesAgenda();

            var resultadoValidacao = this.NovaAgendaUsuarioEhValido();
            if (!resultadoValidacao.IsValid)
                throw new DomainException(string.Join(", ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        public void DefinirUsuarioId(string usuarioId)
        {
            if (usuarioId.EhVazio())
            {
                throw new DomainException("Por favor, certifique-se que adicinou uma pessoa.");
            }

            UsuarioId = usuarioId;
        }


        public void DefinirAgendaId(string agendaId)
        {
            if (agendaId.EhVazio())
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


    public class PermissoesAgenda
    {
        //fazer 
    }
}
