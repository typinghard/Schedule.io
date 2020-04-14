using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public class TipoEventoRepository : Repository<TipoEvento>, ITipoEventoRepository
    {
        public TipoEventoRepository(AgendaContext context) : base(context)
        {

        }

        public override TipoEvento Obter(string tipoEventoId)
        {
            var query = $@"SELECT * 
                           FROM TipoEvento 
                           WHERE Id = '{tipoEventoId}'";

            return base.Obter(query);
        }
    }
}
