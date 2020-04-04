using Raven.Client.Documents.Session;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.RavenDB
{
    public class AgendaUsuarioRepository : Repository<AgendaUsuario>, IAgendaUsuarioRepository
    {
        public AgendaUsuarioRepository(IDocumentSession session) : base(session)
        {
        }

        public AgendaUsuario ObterPorAgendaIdEUsuarioId(string agendaId, string usuarioId)
        {
            return Sessao
                 .Query<AgendaUsuario>()
                 .Where(x => x.AgendaId == agendaId
                        && x.UsuarioId == usuarioId
                        && !x.Inativo)
                 .FirstOrDefault();
        }
    }
}
