using Schedule.io.Core.Messages;
using System;
using Newtonsoft.Json;

namespace Schedule.io.Core.Data.EventSourcing
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
                JsonConvert.SerializeObject(evento));
        }
    }
}
