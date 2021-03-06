﻿using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.SqlServerDB
{
    public class LocalRepository : Repository<Local>, ILocalRepository
    {
        public LocalRepository(AgendaContext context) : base(context)
        {
        }
    }
}
