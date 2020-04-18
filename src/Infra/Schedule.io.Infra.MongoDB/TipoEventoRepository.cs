using Schedule.io.Infra.MongoDB.Configs;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.MongoDB
{
    public class TipoEventoRepository : Repository<TipoEvento>, ITipoEventoRepository
    {
        public TipoEventoRepository(ScheduleioContext context) : base(context)
        {
        }
    }
}
