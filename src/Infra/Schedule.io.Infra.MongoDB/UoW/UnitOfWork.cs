using Schedule.io.Interfaces.Repositories;
using Schedule.io.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.MongoDB.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ScheduleioContext _context;

        public UnitOfWork(ScheduleioContext context)
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
