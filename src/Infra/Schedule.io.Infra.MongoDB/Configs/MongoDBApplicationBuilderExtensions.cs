using Microsoft.Extensions.DependencyInjection;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Core.Data.EventSourcing;
using Schedule.io.Infra.MongoDB.EventSourcing;
using Schedule.io.Interfaces.Repositories;

namespace Schedule.io.Infra.MongoDB.Configs
{
    public static class MongoDBApplicationBuilderExtensions
    {
        public static void UseScheduleIoMongoDb(this IServiceCollection services, MongoDBConfig mongoDBConfig)
        {
            DataBaseConfigurationHelper.SetDataBaseConfig(mongoDBConfig);

            services.AddScoped<ScheduleioContext>();
            services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();

            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();
            services.AddScoped<ILocalRepository, LocalRepository>();
            services.AddScoped<IUnitOfWork, UoW.UnitOfWork>();
        }
    }
}
