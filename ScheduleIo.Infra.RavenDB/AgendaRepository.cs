using Agenda.Domain.Interfaces;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Infra.RavenDB
{
    public class AgendaRepository : Repository<Agenda.Domain.Models.Agenda>, IAgendaRepository
    {
        public AgendaRepository(IDocumentSession session) : base(session)
        {
        }

        public Agenda.Domain.Models.Agenda ObterAgendaPorUsuarioId(string agendaId, string usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
