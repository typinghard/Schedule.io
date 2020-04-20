using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.RavenDB.Indexes
{
    public static class IndexesSetup
    {
        public static IDocumentStore CreateIndexes(this IDocumentStore store)
        {
            return store;
        }
    }
}
