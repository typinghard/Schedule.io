using Microsoft.Extensions.DependencyInjection;
using Raven.DependencyInjection;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Core.Data.EventSourcing;
using Schedule.io.Infra.RavenDB.EventSourcing;
using Schedule.io.Interfaces.Repositories;

namespace Schedule.io.Infra.RavenDB.Configs
{
    public static class RavenDBApplicationBuilderExtensions
    {
        public static void UseScheduleIoRavenDb(this IServiceCollection services, RavenDBConfig ravenDBConfig)
        {
            DataBaseConfigurationHelper.SetDataBaseConfig(ravenDBConfig);

            services
                .AddRavenDbDocStore(options =>
                    options.Settings = new RavenSettings()
                    {
                        Urls = ravenDBConfig.Urls,
                        CertFilePath = ravenDBConfig.CertificateFilePath,
                        DatabaseName = ravenDBConfig.DataBase,
                        CertPassword = ravenDBConfig.CertificatePassword
                    })
                .AddRavenDbSession();

            services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();

            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<ILocalRepository, LocalRepository>();
            services.AddScoped<IUnitOfWork, UoW.UnitOfWork>();
        }
    }
}
