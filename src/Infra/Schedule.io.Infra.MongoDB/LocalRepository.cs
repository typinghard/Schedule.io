using Schedule.io.Infra.MongoDB.Configs;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.MongoDB
{
    public class LocalRepository : Repository<Local>, ILocalRepository
    {
        public LocalRepository(ScheduleioContext context) : base(context)
        {
        }
    }
}
