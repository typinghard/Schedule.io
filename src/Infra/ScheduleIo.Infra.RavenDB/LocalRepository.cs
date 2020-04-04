using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Raven.Client.Documents.Session;
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
