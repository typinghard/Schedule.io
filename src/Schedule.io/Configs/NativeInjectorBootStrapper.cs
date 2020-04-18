using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Schedule.io.Core.Communication.Mediator;
using Schedule.io.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Interfaces.Services;
using Schedule.io.Services;

namespace Schedule.io.Configs
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<IScheduleIo, ScheduleIoService>();
            services.AddScoped<IAgendaService, AgendaService>();
            services.AddScoped<ILocalService, LocalService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEventoService, EventoService>();
        }
    }
}
