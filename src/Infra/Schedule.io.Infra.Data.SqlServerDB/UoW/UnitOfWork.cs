using Schedule.io.Core.Interfaces;

namespace Schedule.io.Infra.Data.SqlServerDB.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AgendaContext _context;

        public UnitOfWork(AgendaContext context)
        {
            _context = context;

        }

        public bool Commit()
        {
            return _context.SalvarAlteracoes() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
