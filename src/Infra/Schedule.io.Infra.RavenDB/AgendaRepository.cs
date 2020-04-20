using Raven.Client.Documents.Session;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.io.Infra.RavenDB
{
    public class AgendaRepository : Repository<Agenda>, IAgendaRepository
    {
        public AgendaRepository(IDocumentSession session) : base(session)
        {
        }

        public void Gravar(AgendaUsuario agendaUsuario)
        {
            _session.Store(agendaUsuario);
        }

        public IList<Agenda> Listar(string usuarioId)
        {
            return Sessao
                   .Query<Agenda>()
                   .Where(x => x.UsuarioIdCriador == usuarioId)
                   .ToList();
        }

        public Agenda Obter(string agendaId, string usuarioId)
        {
            return Sessao.Query<Agenda>()
                .Where(a => a.AgendasUsuarios.Any(y => y.UsuarioId == usuarioId)
                         && a.Id == agendaId)
                .FirstOrDefault();
        }

        public bool VerificaSeAgendaExiste(string agendaId)
        {
            return Sessao
                .Query<Agenda>()
                .Any(x => x.Id == agendaId);
        }
    }
}
