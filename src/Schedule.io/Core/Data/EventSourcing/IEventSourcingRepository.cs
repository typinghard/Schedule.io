using Schedule.io.Core.Messages;
using System.Collections.Generic;

namespace Schedule.io.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        void SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        IList<StoredEvent> ObterEventos(string aggregateId);
    }
}
