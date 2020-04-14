using Raven.Client.Documents.Session;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Interfaces.Services;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.RavenDB
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IDocumentSession session) : base(session)
        {
        }

        public bool VerificaSeUsuarioExiste(string usuarioId)
        {
            return Sessao
                   .Query<Usuario>()
                   .Where(x => x.Id == usuarioId)
                   .FirstOrDefault() == null ? false : true;
        }
    }
}
