using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Infra.RavenDB
{
    public class EventoUsuarioRepository : Repository<EventoUsuario>, IEventoUsuarioRepository
    {
        public EventoUsuarioRepository(IDocumentSession session) : base(session)
        {
        }
    }
}
