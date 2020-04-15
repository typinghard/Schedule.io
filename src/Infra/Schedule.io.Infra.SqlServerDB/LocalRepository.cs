using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public class LocalRepository : Repository<Local>, ILocalRepository
    {
        public LocalRepository(AgendaContext context) : base(context)
        {
        }

        public override Local Obter(string localId)
        {
            var query = $@"SELECT * 
                           FROM Local 
                           WHERE Id = '{localId}'";

            return base.Obter(query);
        }
    }
}
