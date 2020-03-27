using Agenda.Core.Data.EventSourcing;
using Agenda.Domain.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Agenda.Domain.Core.Data.EventSourcing
{
    public static class EventSourcingConfigurationHelper
    {
        public static bool Use { get; private set; }
        public static void SetUse(bool use)
        {
            Use = use;
        }


        public static StoredEvent FormatarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            return new StoredEvent(
                Guid.NewGuid().ToString(),
                evento.AggregateId,
                evento.MessageType,
                evento.Timestamp,
                JsonSerializer.Serialize(evento));
        }
    }
}
