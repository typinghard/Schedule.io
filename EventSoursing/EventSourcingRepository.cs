using System;
using System.Collections.Generic;
using System.Linq;
using Agenda.Core.Data.EventSourcing;
using Agenda.Domain.Core.Messages;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Agenda.Infra.Data;

namespace EventSoursing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        protected AgendaContext _agendaContext { get; private set; }
        protected IMongoCollection<StoredEvent> _collection { get; private set; }
        protected string _collectionName { get; private set; }

        public EventSourcingRepository(AgendaContext agendaContext)
        {
            _agendaContext = agendaContext;
            setCollectionName();
            setConnectAndCollection();
        }
        #region internal
        internal void setCollectionName()
        {
            _collectionName = "storedevent";
        }
        internal void setConnectAndCollection()
        {
            _collection = _agendaContext.StoredEvents;
        }
        #endregion

        public void SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            _collection.InsertOne(FormatarEvento(evento));
        }
        public IList<StoredEvent> ObterEventos(Guid aggregateId)
        {
            IMongoQueryable<StoredEvent> query = _collection.AsQueryable();
            return query.Where(x => x.AggregatedId == aggregateId).OrderBy(x => x.DataOcorrencia).ToList();
        }

        private static StoredEvent FormatarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            return new StoredEvent(
                Guid.NewGuid(),
                evento.AggregateId,
                evento.MessageType,
                evento.Timestamp,
                JsonSerializer.Serialize(evento));
        }
    }

    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}
