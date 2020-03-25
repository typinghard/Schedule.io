using Agenda.Application.Interfaces;
using Agenda.Application.Services;
using Agenda.Core.Data.EventSourcing;
using Agenda.Domain.CommandHandlers;
using Agenda.Domain.Commands;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;
using Agenda.Domain.EventHandlers;
using Agenda.Domain.Events;
using Agenda.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ScheduleIo.Infra.Configurations;
using ScheduleIo.Infra.MongoDB.EventSourcing;
using ScheduleIo.Infra.RavenDB.EventSourcing;

namespace Agenda.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ScheduleIo.Infra.MongoDB.AgendaContext>();
            switch (DataBaseConfigurationHelper.DataBaseConfig.GetDataBaseType())
            {
                case ScheduleIo.Infra.Configurations.Enums.EDataBaseType.MONGODB:
                    RegisterMongoDbServices(services);
                    break;
                case ScheduleIo.Infra.Configurations.Enums.EDataBaseType.RAVENDB:
                    RegisterRavenDbServices(services);
                    break;
            }

            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            

            // Application
            services.AddScoped<IAgendaAppService, AgendaAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IAgendaUsuarioAppService, AgendaUsuarioAppService>();
            services.AddScoped<IEventoAgendaAppService, EventoAgendaAppService>();
            services.AddScoped<IEventoUsuarioAppService, EventoUsuarioAppService>();
            services.AddScoped<ILocalAppService, LocalAppService>();

            /* ASSIM COMO NO CONDOMINIO_SERVICE, NÃO DEVE SER NECESSÁRIO ADICIONAR EVENTO OU COMANDOS */
            //Domain - Events
            services.AddScoped<INotificationHandler<AgendaRegistradaEvent>, AgendaEventHandler>();
            services.AddScoped<INotificationHandler<AgendaAtualizadaEvent>, AgendaEventHandler>();
            services.AddScoped<INotificationHandler<AgendaRemovidaEvent>, AgendaEventHandler>();

            services.AddScoped<INotificationHandler<UsuarioRegistradoEvent>, UsuarioEventHandler>();
            services.AddScoped<INotificationHandler<UsuarioAtualizadoEvent>, UsuarioEventHandler>();
            services.AddScoped<INotificationHandler<UsuarioRemovidoEvent>, UsuarioEventHandler>();

            services.AddScoped<INotificationHandler<AgendaUsuarioRegistradoEvent>, AgendaUsuarioEventHandler>();
            services.AddScoped<INotificationHandler<AgendaUsuarioAtualizadoEvent>, AgendaUsuarioEventHandler>();
            services.AddScoped<INotificationHandler<AgendaUsuarioRemovidoEvent>, AgendaUsuarioEventHandler>();

            services.AddScoped<INotificationHandler<EventoAgendaRegistradoEvent>, EventoAgendaEventHandler>();
            services.AddScoped<INotificationHandler<EventoAgendaAtualizadoEvent>, EventoAgendaEventHandler>();
            services.AddScoped<INotificationHandler<EventoAgendaRemovidoEvent>, EventoAgendaEventHandler>();

            services.AddScoped<INotificationHandler<EventoUsuarioRegistradoEvent>, EventoUsuarioEventHandler>();
            services.AddScoped<INotificationHandler<EventoUsuarioAtualizadoEvent>, EventoUsuarioEventHandler>();
            services.AddScoped<INotificationHandler<EventoUsuarioRemovidoEvent>, EventoUsuarioEventHandler>();

            services.AddScoped<INotificationHandler<LocalRegistradoEvent>, LocalEventHandler>();
            services.AddScoped<INotificationHandler<LocalAtualizadoEvent>, LocalEventHandler>();
            services.AddScoped<INotificationHandler<LocalRemovidoEvent>, LocalEventHandler>();

            // Domain - Commands
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

            services.AddScoped<IRequestHandler<RegistrarEventoUsuarioCommand, bool>, EventoUsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarEventoUsuarioCommand, bool>, EventoUsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverEventoUsuarioCommand, bool>, EventoUsuarioCommandHandler>();

            services.AddScoped<IRequestHandler<RegistrarLocalCommand, bool>, LocalCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarLocalCommand, bool>, LocalCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverLocalCommand, bool>, LocalCommandHandler>();
        }

        private static void RegisterMongoDbServices(IServiceCollection services)
        {
            services.AddScoped<IEventSourcingRepository, ScheduleIo.Infra.MongoDB.EventSourcing.EventSourcingRepository>();

            
            services.AddScoped<IAgendaRepository, ScheduleIo.Infra.MongoDB.AgendaRepository>();
            services.AddScoped<IUsuarioRepository, ScheduleIo.Infra.MongoDB.UsuarioRepository>();
            services.AddScoped<IAgendaUsuarioRepository, ScheduleIo.Infra.MongoDB.AgendaUsuarioRepository>();
            services.AddScoped<IEventoAgendaRepository, ScheduleIo.Infra.MongoDB.EventoAgendaRepository>();
            services.AddScoped<IEventoUsuarioRepository, ScheduleIo.Infra.MongoDB.EventoUsuarioRepository>();
            services.AddScoped<ILocalRepository, ScheduleIo.Infra.MongoDB.LocalRepository>();
            services.AddScoped<IUnitOfWork, ScheduleIo.Infra.MongoDB.UoW.UnitOfWork>();
        }
        private static void RegisterRavenDbServices(IServiceCollection services)
        {
            services.AddScoped<IEventSourcingRepository, ScheduleIo.Infra.RavenDB.EventSourcing.EventSourcingRepository>();

            services.AddScoped<IAgendaRepository, ScheduleIo.Infra.RavenDB.AgendaRepository>();
            services.AddScoped<IUsuarioRepository, ScheduleIo.Infra.RavenDB.UsuarioRepository>();
            services.AddScoped<IAgendaUsuarioRepository, ScheduleIo.Infra.RavenDB.AgendaUsuarioRepository>();
            services.AddScoped<IEventoAgendaRepository, ScheduleIo.Infra.RavenDB.EventoAgendaRepository>();
            services.AddScoped<IEventoUsuarioRepository, ScheduleIo.Infra.RavenDB.EventoUsuarioRepository>();
            services.AddScoped<ILocalRepository, ScheduleIo.Infra.RavenDB.LocalRepository>();
            services.AddScoped<IUnitOfWork, ScheduleIo.Infra.RavenDB.UoW.UnitOfWork>();
        }
    }
}
