using Raven.Client.Documents.Session;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;
using System.Linq;

namespace Schedule.io.Infra.RavenDB
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IDocumentSession session) : base(session)
        {
        }
    }
}
