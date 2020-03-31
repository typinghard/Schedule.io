using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleIo.Infra.RavenDB
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
                   .Where(x => x.EventoId == eventoId)
                   .ToList();
        }
    }
}
