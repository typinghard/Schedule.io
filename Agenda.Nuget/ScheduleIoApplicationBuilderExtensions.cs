using Agenda.Domain.Core.Data.EventSourcing;
using Agenda.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Raven.DependencyInjection;
using ScheduleIo.Infra.Configurations;
using ScheduleIo.Infra.RavenDB.Configs;
using ScheduleIo.Nuget.Interfaces;
using ScheduleIo.Nuget.Models;
using ScheduleIo.Nuget.Services;

namespace ScheduleIo.Nuget
{
    public static class ScheduleIoApplicationBuilderExtensions
    {

        public static void UseScheduleIo(this IApplicationBuilder app, ScheduleIoConfigurations scheduleIoConfigurations)
        {

        }
        public static IServiceCollection AddScheduleIo(this IServiceCollection services, ScheduleIoConfigurations scheduleIoConfigurations)
        {
            SetConfigurations(scheduleIoConfigurations);

            RegisterDataBaseServices(services);
            RegisterScheduleIoServices(services);

            NativeInjectorBootStrapper.RegisterServices(services);
            return services;
        }

        private static void SetConfigurations(ScheduleIoConfigurations scheduleIoConfigurations)
        {
            DataBaseConfigurationHelper.SetDataBaseConfig(scheduleIoConfigurations.DataBaseConfig);
            EventSourcingConfigurationHelper.SetUse(scheduleIoConfigurations.UseEventSourcing);
        }
        private static void RegisterDataBaseServices(IServiceCollection services)
        {
            if (DataBaseConfigurationHelper.DataBaseConfig.GetDataBaseType() == Infra.Configurations.Enums.EDataBaseType.RAVENDB)
            {
                var ravenDbConfig = ((RavenDBConfig)DataBaseConfigurationHelper.DataBaseConfig);
                services
                    .AddRavenDbDocStore(options =>
                        options.Settings = new RavenSettings()
                        {
                            Urls = ravenDbConfig.Urls,
                            CertFilePath = ravenDbConfig.CertificateFilePath,
                            DatabaseName = ravenDbConfig.DataBase,
                            CertPassword = ravenDbConfig.CertificatePassword })
                    .AddRavenDbSession();
            }
        }
        private static void RegisterScheduleIoServices(IServiceCollection services)
        {

            services.AddScoped<IScheduleIo, ScheduleIoService>();
            services.AddScoped<IAgendaService, AgendaService>();
            services.AddScoped<ILocalService, LocalService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEventoService, EventoService>();
        }
    }
}
