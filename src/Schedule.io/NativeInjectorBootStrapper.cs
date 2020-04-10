using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Schedule.io.Core.CommandHandlers;
using Schedule.io.Core.Commands.AgendaCommands;
using Schedule.io.Core.Commands.AgendaUsuarioCommands;
using Schedule.io.Core.Commands.ConviteCommands;
using Schedule.io.Core.Commands.EventoAgendaCommands;
using Schedule.io.Core.Commands.LocalCommands;
using Schedule.io.Core.Commands.UsuarioCommands;
using Schedule.io.Core.Core.Communication.Mediator;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Core.Events.AgendaEvents;
using Schedule.io.Interfaces;
using Schedule.io.Services;

namespace Schedule.io
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
            ////Domain - Events
            //services.AddScoped<INotificationHandler<AgendaRegistradaEvent>, AgendaEventHandler>();
            //services.AddScoped<INotificationHandler<AgendaAtualizadaEvent>, AgendaEventHandler>();
            //services.AddScoped<INotificationHandler<AgendaRemovidaEvent>, AgendaEventHandler>();

            //services.AddScoped<INotificationHandler<UsuarioRegistradoEvent>, UsuarioEventHandler>();
            //services.AddScoped<INotificationHandler<UsuarioAtualizadoEvent>, UsuarioEventHandler>();
            //services.AddScoped<INotificationHandler<UsuarioRemovidoEvent>, UsuarioEventHandler>();

            //services.AddScoped<INotificationHandler<AgendaUsuarioRegistradoEvent>, AgendaUsuarioEventHandler>();
            //services.AddScoped<INotificationHandler<AgendaUsuarioAtualizadoEvent>, AgendaUsuarioEventHandler>();
            //services.AddScoped<INotificationHandler<AgendaUsuarioRemovidoEvent>, AgendaUsuarioEventHandler>();

            //services.AddScoped<INotificationHandler<EventoAgendaRegistradoEvent>, EventoAgendaEventHandler>();
            //services.AddScoped<INotificationHandler<EventoAgendaAtualizadoEvent>, EventoAgendaEventHandler>();
            //services.AddScoped<INotificationHandler<EventoAgendaRemovidoEvent>, EventoAgendaEventHandler>();

            //services.AddScoped<INotificationHandler<ConviteRegistradoEvent>, ConviteEventHandler>();
            //services.AddScoped<INotificationHandler<ConviteAtualizadoEvent>, ConviteEventHandler>();
            //services.AddScoped<INotificationHandler<ConviteRemovidoEvent>, ConviteEventHandler>();

            //services.AddScoped<INotificationHandler<LocalRegistradoEvent>, LocalEventHandler>();
            //services.AddScoped<INotificationHandler<LocalAtualizadoEvent>, LocalEventHandler>();
            //services.AddScoped<INotificationHandler<LocalRemovidoEvent>, LocalEventHandler>();

            //// Domain - Commands
            services.AddScoped<IRequestHandler<RegistrarAgendaCommand, bool>, AgendaCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarAgendaCommand, bool>, AgendaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverAgendaCommand, bool>, AgendaCommandHandler>();

            services.AddScoped<IRequestHandler<RegistrarUsuarioCommand, bool>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarUsuarioCommand, bool>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverUsuarioCommand, bool>, UsuarioCommandHandler>();

            services.AddScoped<IRequestHandler<RegistrarAgendaUsuarioCommand, bool>, AgendaUsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarAgendaUsuarioCommand, bool>, AgendaUsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverAgendaUsuarioCommand, bool>, AgendaUsuarioCommandHandler>();

            services.AddScoped<IRequestHandler<RegistrarEventoAgendaCommand, bool>, EventoAgendaCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarEventoAgendaCommand, bool>, EventoAgendaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverEventoAgendaCommand, bool>, EventoAgendaCommandHandler>();

            services.AddScoped<IRequestHandler<RegistrarConviteCommand, bool>, ConviteCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarConviteCommand, bool>, ConviteCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverConviteCommand, bool>, ConviteCommandHandler>();

            services.AddScoped<IRequestHandler<RegistrarLocalCommand, bool>, LocalCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarLocalCommand, bool>, LocalCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverLocalCommand, bool>, LocalCommandHandler>();
        }
    }
}
