using Raven.Client.Documents.Queries;
using Raven.Client.Documents.Session;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
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

            return (
                from a in Sessao.Query<Agenda>()
                let u = RavenQuery.Load<AgendaUsuario>(a.Id)
                where u.UsuarioId == usuarioId
                select a
                ).FirstOrDefault();

            //var agendaUsuario = Sessao
            //     .Query<AgendaUsuario>()
            //     .Where(x => x.AgendaId == agendaId && x.UsuarioId == usuarioId)
            //     .FirstOrDefault();

            //if (agendaUsuario == null)
            //    return null;

            //return Sessao
            //    .Query<Agenda>()
            //    .Where(x => x.Id == agendaId)
            //    .FirstOrDefault();
        }
    }
}
