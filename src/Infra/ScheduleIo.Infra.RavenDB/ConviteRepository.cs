using Raven.Client.Documents.Session;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.RavenDB
{
    public class ConviteRepository : Repository<Convite>, IConviteRepository
    {
        public ConviteRepository(IDocumentSession session) : base(session)
        {
        }

        public IList<Convite> ObterConvitesPorEventoId(string eventoId)
        {
            return Sessao
                   .Query<Convite>()
                   .Where(x => x.EventoId == eventoId
                          && !x.Inativo)
                   .ToList();
        }
    }
}
