using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Models
{
    public class AgendaUsuario : Entity, IAggregateRoot
    {
        public string AgendaId { get; protected set; }
        public string UsuarioId { get; protected set; }

        public AgendaUsuario(string agendaId, string usuarioId)
        {
            AgendaId = agendaId;
            UsuarioId = usuarioId;
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
            if (AgendaId.EhVazio())
            {
                throw new DomainException("Por favor, certifique-se que adicinou uma agenda.");
            }

            AgendaId = agendaId;
        }
    }
}
