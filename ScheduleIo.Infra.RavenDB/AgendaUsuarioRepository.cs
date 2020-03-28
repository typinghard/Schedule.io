using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Infra.RavenDB
{
    public class AgendaUsuarioRepository : Repository<AgendaUsuario>, IAgendaUsuarioRepository
    {
        public AgendaUsuarioRepository(IDocumentSession session) : base(session)
        {
        }

        public AgendaUsuario ObterPorId(string agendaId, string usuarioId)
        {
            var DocumentName = "agendausuario";

            return _session.Advanced.RawQuery<AgendaUsuario>(
           "from " + DocumentName + " " +
           "where agendaId == $agendaId " + " " +
           "and usuarioId == $usuarioId")
               .AddParameter("agendaId", agendaId.ToString())
               .AddParameter("usuarioId", usuarioId.ToString())
               .FirstOrDefault();
        }
    }
}
