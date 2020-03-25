using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.Core.Messages;

namespace Agenda.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        void SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        IList<StoredEvent> ObterEventos(string aggregateId);
    }
}
