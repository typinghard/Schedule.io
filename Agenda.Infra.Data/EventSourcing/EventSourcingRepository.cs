using Agenda.Core.Data.EventSourcing;
using Agenda.Domain.Core.Data.EventSourcing;
using Agenda.Domain.Core.Messages;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ScheduleIo.Infra.MongoDB.EventSourcing
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
            _collectionName = "storedevent_scheduleio";
        }
        internal void setConnectAndCollection()
        {
            _collection = _agendaContext.StoredEvents;
        }
        #endregion

        public void SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            if (EventSourcingConfigurationHelper.Use)
                _collection.InsertOne(EventSourcingConfigurationHelper.FormatarEvento(evento));
        }
        public IList<StoredEvent> ObterEventos(string aggregateId)
        {
            IMongoQueryable<StoredEvent> query = _collection.AsQueryable();
            return query.Where(x => x.AggregatedId == aggregateId).OrderBy(x => x.DataOcorrencia).ToList();
        }
    }
}
