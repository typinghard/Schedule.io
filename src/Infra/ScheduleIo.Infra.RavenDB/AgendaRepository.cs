using Raven.Client.Documents.Session;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.RavenDB
{
    public class AgendaRepository : Repository<Agenda>, IAgendaRepository
    {
        public AgendaRepository(IDocumentSession session) : base(session)
        {
        }

        public Agenda ObterAgendaPorUsuarioId(string agendaId, string usuarioId)
        {
            var agendaUsuario = Sessao
                 .Query<AgendaUsuario>()
                 .Where(x => x.AgendaId == agendaId && x.UsuarioId == usuarioId)
                 .FirstOrDefault();

            if (agendaUsuario == null)
                return null;

            return Sessao
                .Query<Agenda>()
                .Where(x => x.Id == agendaId)
                .FirstOrDefault();
        }
    }
}
