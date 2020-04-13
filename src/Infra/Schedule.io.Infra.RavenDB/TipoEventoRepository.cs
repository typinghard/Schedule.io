using Raven.Client.Documents.Session;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.RavenDB
{
    public class TipoEventoRepository : Repository<TipoEvento>, ITipoEventoRepository
    {
        public TipoEventoRepository(IDocumentSession session) : base(session)
        {
        }
    }
}
