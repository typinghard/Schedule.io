using Agenda.Core.Data.EventSourcing;
using Agenda.Domain.Core.Data.Configurations;
using Agenda.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Raven.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
