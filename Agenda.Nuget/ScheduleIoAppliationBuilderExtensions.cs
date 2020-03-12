using Agenda.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ScheduleIo.Nuget.Interfaces;
using ScheduleIo.Nuget.Models;
using ScheduleIo.Nuget.Services;

namespace ScheduleIo.Nuget
{
    public static class ScheduleIoAppliationBuilderExtensions
    {
        public static IServiceCollection AddScheduleIo(this IServiceCollection services, ScheduleIoConfigurations scheduleIoConfigurations)
        {
            ScheduleIoConfigurationHelper.SetDataBaseConfig(scheduleIoConfigurations.DataBaseConfig);
            
            RegisterScheduleIoServices(services);
            NativeInjectorBootStrapper.RegisterServices(services);
            return services;
        }

        private static void RegisterScheduleIoServices(IServiceCollection services)
        {
            services.AddSingleton<IDataBaseConfigurationService, DataBaseConfigurationService>();

            services.AddScoped<IScheduleIo, ScheduleIoService>();
            services.AddScoped<IAgendaService, AgendaService>();
            services.AddScoped<ILocalService, LocalService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEventoService, EventoService>();
        }
    }
}
