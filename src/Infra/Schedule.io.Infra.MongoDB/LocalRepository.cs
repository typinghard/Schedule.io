using Schedule.io.Interfaces.Repositories;
using Schedule.io.Interfaces.Services;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.MongoDB
{
    public class LocalRepository : Repository<Local>, ILocalRepository
    {
        public LocalRepository(AgendaContext context) : base(context)
        {
        }
    }
}
