using Agenda.Domain.Interfaces;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var agendaUsuario = Sessao
                 .Query<Agenda.Domain.Models.AgendaUsuario>()
                 .Where(x => x.AgendaId == agendaId && x.UsuarioId == usuarioId)
                 .FirstOrDefault();

            if (agendaUsuario == null)
                return null;

            return Sessao
                .Query<Agenda.Domain.Models.Agenda>()
                .Where(x => x.Id == agendaId)
                .FirstOrDefault();
        }
    }
}
