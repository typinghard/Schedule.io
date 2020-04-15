using Raven.Client.Documents.Session;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.RavenDB
{
    public class LocalRepository : Repository<Local>, ILocalRepository
    {
        public LocalRepository(IDocumentSession session) : base(session)
        {
        }
    }
}
