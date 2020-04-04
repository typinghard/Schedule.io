using Schedule.io.Core.Core.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.io.Core.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        void SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        IList<StoredEvent> ObterEventos(string aggregateId);
    }
}
