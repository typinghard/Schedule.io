using EventStore.ClientAPI;

namespace EventSoursing
{
    public interface IEventStoreService
    {
        IEventStoreConnection GetConnection();
    }
}
