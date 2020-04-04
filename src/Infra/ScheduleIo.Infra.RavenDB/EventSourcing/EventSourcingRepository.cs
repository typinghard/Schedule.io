using Agenda.Core.Data.EventSourcing;
using Agenda.Domain.Core.Data.EventSourcing;
using Agenda.Domain.Core.Messages;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Schedule.io.Infra.RavenDB.EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        protected readonly IDocumentSession _session;

        public EventSourcingRepository(IDocumentSession session)
        {
            _session = session;
        }

        public void SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            if (EventSourcingConfigurationHelper.Use)
                _session.Store(EventSourcingConfigurationHelper.FormatarEvento(evento));
        }
        public IList<StoredEvent> ObterEventos(string aggregateId)
        {
            return (
                from storedevent in _session.Query<StoredEvent>()
                where storedevent.AggregatedId == aggregateId
                select storedevent
                ).ToList();
        }
    }
}
