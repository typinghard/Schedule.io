using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Schedule.io.Core.Core.Data.Configurations;
using Schedule.io.Core.Core.Data.EventSourcing;
using Schedule.io.Core.Interfaces;
using Schedule.io.Infra.Data.SqlServerDB.EventSourcing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB.Configs
{
    public static class SqlServerDBApplicationBuilderExtensions
    {
        public static void UseScheduleIoSqlServerDb(this IServiceCollection services, SqlServerDBConfig sqlServerDBConfig)
        {
            DataBaseConfigurationHelper.SetDataBaseConfig(sqlServerDBConfig);

            services.AddDbContext<AgendaContext>(options =>
                options.UseSqlServer(sqlServerDBConfig.ConnectionsString));

            services.AddScoped<AgendaContext>();
            services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();
            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAgendaUsuarioRepository, AgendaUsuarioRepository>();
            services.AddScoped<IEventoAgendaRepository, EventoAgendaRepository>();
            services.AddScoped<IConviteRepository, ConviteRepository>();
            services.AddScoped<ILocalRepository, LocalRepository>();
            services.AddScoped<IUnitOfWork, UoW.UnitOfWork>();
        }
    }
}
