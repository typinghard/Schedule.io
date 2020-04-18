using Raven.Client.Documents.Session;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.RavenDB
{
    public class TipoEventoRepository : Repository<TipoEvento>, ITipoEventoRepository
    {
        public TipoEventoRepository(IDocumentSession session) : base(session)
        {
        }
    }
}
