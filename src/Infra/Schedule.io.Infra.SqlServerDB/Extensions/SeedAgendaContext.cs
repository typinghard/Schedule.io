using Microsoft.Extensions.DependencyInjection;
using Schedule.io.Infra.SqlServerDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.SqlServerDB.Extensions
{
    public static class SeedAgendaContext
    {
        public static void CriarTabelas(AgendaContext agendaContext)
        {
            agendaContext.Database.EnsureCreated();
        }
    }
}
