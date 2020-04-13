using Raven.Client.Documents.Session;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.RavenDB
{
    public class LocalRepository : Repository<Local>, ILocalRepository
    {
        public LocalRepository(IDocumentSession session) : base(session)
        {
        }
    }
}
