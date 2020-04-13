using Schedule.io.Core.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.io.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        void SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        IList<StoredEvent> ObterEventos(string aggregateId);
    }
}
