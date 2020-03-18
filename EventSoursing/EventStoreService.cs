using Agenda.Infra.Data.Configs;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;

namespace EventSoursing
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreConnection _connection;

        public EventStoreService()
        {
            _connection = EventStoreConnection.Create(
                ScheduleIoConfigurationHelper.DataBaseConfig.ConnectionString);

            _connection.ConnectAsync();
        }

        public IEventStoreConnection GetConnection()
        {
            return _connection;
        }
    }
}
