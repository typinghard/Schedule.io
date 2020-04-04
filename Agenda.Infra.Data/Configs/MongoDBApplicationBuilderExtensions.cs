using Agenda.Core.Data.EventSourcing;
using Agenda.Domain.Core.Data.Configurations;
using Agenda.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Infra.MongoDB.Configs
{
    public static class MongoDBApplicationBuilderExtensions
    {
        public static void UseScheduleIoMongoDb(this IServiceCollection services, MongoDBConfig mongoDBConfig)
        {
            DataBaseConfigurationHelper.SetDataBaseConfig(mongoDBConfig);

            services.AddScoped<AgendaContext>();
            services.AddScoped<IEventSourcingRepository, EventSourcing.EventSourcingRepository>();
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
