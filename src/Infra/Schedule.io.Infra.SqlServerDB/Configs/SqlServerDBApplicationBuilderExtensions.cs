﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Core.Data.EventSourcing;
using Schedule.io.Infra.SqlServerDB.EventSourcing;
using Schedule.io.Infra.SqlServerDB.Extensions;
using Schedule.io.Interfaces.Repositories;
using System;

namespace Schedule.io.Infra.SqlServerDB.Configs
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
            services.AddScoped<ILocalRepository, LocalRepository>();
            services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();
            services.AddScoped<IEventoAgendaRepository, EventoAgendaRepository>();
            services.AddScoped<IUnitOfWork, UoW.UnitOfWork>();
        }
    }
}
