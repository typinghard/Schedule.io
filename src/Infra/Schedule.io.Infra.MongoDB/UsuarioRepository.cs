using MongoDB.Driver;
using Schedule.io.Infra.MongoDB.Configs;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.MongoDB
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ScheduleioContext context) : base(context)
        {

        }
    }
}
