using Raven.Client.Documents.Session;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.RavenDB
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IDocumentSession session) : base(session)
        {
        }
    }
}
