using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Schedule.io.Core.Core.Data.EventSourcing;
using Schedule.io.Models;
using System;

namespace Schedule.io
{
    public static class ScheduleIoApplicationBuilderExtensions
    {
        public static IServiceCollection AddScheduleIo(this IServiceCollection services, ScheduleIoConfigurations scheduleIoConfigurations)
        {
            var assembly = AppDomain.CurrentDomain.Load("Agenda.Domain");
            services.AddMediatR(assembly);

            SetConfigurations(scheduleIoConfigurations);
            NativeInjectorBootStrapper.RegisterServices(services);
            return services;
        }

        private static void SetConfigurations(ScheduleIoConfigurations scheduleIoConfigurations)
        {
            EventSourcingConfigurationHelper.SetUse(scheduleIoConfigurations.UseEventSourcing);
        }
    }
}
