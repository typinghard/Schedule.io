﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Schedule.io.Core.Data.EventSourcing;
using System;

namespace Schedule.io.Configs
{
    public static class ScheduleIoApplicationBuilderExtensions
    {
        public static IServiceCollection AddScheduleIo(this IServiceCollection services, ScheduleIoConfigurations scheduleIoConfigurations)
        {
            var assembly = AppDomain.CurrentDomain.Load("Schedule.io");
            services.AddMediatR(assembly);

            SetConfigurations(scheduleIoConfigurations);
            NativeInjectorBootStrapper.RegisterServices(services);
            return services;
        }

        private static void SetConfigurations(ScheduleIoConfigurations scheduleIoConfigurations)
        {
            EventSourcingConfigurationHelper.Use = scheduleIoConfigurations.UseEventSourcing;
            ScheduleIoConfigurationHelper.BloquearEventosOcupadosNoMesmoHorario = scheduleIoConfigurations.BloquearEventosOcupadosNoMesmoHorario;
            ScheduleIoConfigurationHelper.BloquearEventosOcupadosNoMesmoLocal = scheduleIoConfigurations.BloquearEventosOcupadosNoMesmoLocal;
            ScheduleIoConfigurationHelper.BloquearEventosOcupadosNoMesmoHorarioParaConvidados = scheduleIoConfigurations.BloquearEventosOcupadosNoMesmoHorarioParaConvidados;
            ScheduleIoConfigurationHelper.ValidarLimiteDeQuantidadePorLocal = scheduleIoConfigurations.ValidarLimiteDeQuantidadePorLocal;
        }
    }
}
