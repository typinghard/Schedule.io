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

            /* ASSIM COMO NO CONDOMINIO_SERVICE, NÃO DEVE SER NECESSÁRIO ADICIONAR EVENTO OU COMANDOS */
            //Domain - Events

            //// Domain - Commands
            //services.AddScoped<IRequestHandler<RegistrarAgendaCommand, bool>, AgendaCommandHandler>();
            //services.AddScoped<IRequestHandler<AtualizarAgendaCommand, bool>, AgendaCommandHandler>();
            //services.AddScoped<IRequestHandler<RemoverAgendaCommand, bool>, AgendaCommandHandler>();

            //services.AddScoped<IRequestHandler<RegistrarUsuarioCommand, bool>, UsuarioCommandHandler>();
            //services.AddScoped<IRequestHandler<AtualizarUsuarioCommand, bool>, UsuarioCommandHandler>();
            //services.AddScoped<IRequestHandler<RemoverUsuarioCommand, bool>, UsuarioCommandHandler>();

            //services.AddScoped<IRequestHandler<RegistrarAgendaUsuarioCommand, bool>, AgendaUsuarioCommandHandler>();
            //services.AddScoped<IRequestHandler<AtualizarAgendaUsuarioCommand, bool>, AgendaUsuarioCommandHandler>();
            //services.AddScoped<IRequestHandler<RemoverAgendaUsuarioCommand, bool>, AgendaUsuarioCommandHandler>();

            //services.AddScoped<IRequestHandler<RegistrarEventoAgendaCommand, bool>, EventoAgendaCommandHandler>();
            //services.AddScoped<IRequestHandler<AtualizarEventoAgendaCommand, bool>, EventoAgendaCommandHandler>();
            //services.AddScoped<IRequestHandler<RemoverEventoAgendaCommand, bool>, EventoAgendaCommandHandler>();

            //services.AddScoped<IRequestHandler<RegistrarConviteCommand, bool>, ConviteCommandHandler>();
            //services.AddScoped<IRequestHandler<AtualizarConviteCommand, bool>, ConviteCommandHandler>();
            //services.AddScoped<IRequestHandler<RemoverConviteCommand, bool>, ConviteCommandHandler>();

            //services.AddScoped<IRequestHandler<RegistrarLocalCommand, bool>, LocalCommandHandler>();
            //services.AddScoped<IRequestHandler<AtualizarLocalCommand, bool>, LocalCommandHandler>();
            //services.AddScoped<IRequestHandler<RemoverLocalCommand, bool>, LocalCommandHandler>();
        }
    }
}
