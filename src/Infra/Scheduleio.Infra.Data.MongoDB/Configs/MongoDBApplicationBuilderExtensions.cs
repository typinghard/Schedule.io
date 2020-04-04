﻿using Microsoft.Extensions.DependencyInjection;
using Schedule.io.Core.Core.Data.Configurations;
using Schedule.io.Core.Core.Data.EventSourcing;
using Schedule.io.Core.Interfaces;
using Schedule.io.Infra.Data.MongoDB.EventSourcing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.Data.MongoDB.Configs
{
    public static class MongoDBApplicationBuilderExtensions
    {
        public static void UseScheduleIoMongoDb(this IServiceCollection services, MongoDBConfig mongoDBConfig)
        {
            DataBaseConfigurationHelper.SetDataBaseConfig(mongoDBConfig);

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
