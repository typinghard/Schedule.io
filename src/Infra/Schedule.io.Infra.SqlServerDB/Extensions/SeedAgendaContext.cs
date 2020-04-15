using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB.Extensions
{
    public static class SeedAgendaContext
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AgendaContext>();
            context.Database.EnsureCreated();
        }
    }
}
