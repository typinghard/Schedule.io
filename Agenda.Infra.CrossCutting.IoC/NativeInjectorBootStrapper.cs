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
using Agenda.Infra.Data;
using Agenda.Infra.Data.Configs;
using Agenda.Infra.Data.UoW;
using EventSoursing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Event Sourcing
            services.AddScoped<IEventStoreService, EventStoreService>();
            services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();

            //Evento Agenda Context
            services.AddScoped<AgendaContext>();

            //Repository
            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAgendaUsuarioRepository, AgendaUsuarioRepository>();
            services.AddScoped<IEventoAgendaRepository, EventoAgendaRepository>();
            services.AddScoped<IConviteRepository, ConviteRepository>();
            services.AddScoped<ILocalRepository, LocalRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Application
            services.AddScoped<IAgendaAppService, AgendaAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IAgendaUsuarioAppService, AgendaUsuarioAppService>();
            services.AddScoped<IEventoAgendaAppService, EventoAgendaAppService>();
            services.AddScoped<IConviteAppService, ConviteAppService>();
            services.AddScoped<ILocalAppService, LocalAppService>();

            /* ASSIM COMO NO CONDOMINIO_SERVICE, NÃO DEVE SER NECESSÁRIO ADICIONAR EVENTO OU COMANDOS */
            // Domain - Events
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

            services.AddScoped<INotificationHandler<ConviteRegistradoEvent>, ConviteEventHandler>();
            services.AddScoped<INotificationHandler<ConviteAtualizadoEvent>, ConviteEventHandler>();
            services.AddScoped<INotificationHandler<ConviteRemovidoEvent>, ConviteEventHandler>();

            services.AddScoped<INotificationHandler<LocalRegistradoEvent>, LocalEventHandler>();
            services.AddScoped<INotificationHandler<LocalAtualizadoEvent>, LocalEventHandler>();
            services.AddScoped<INotificationHandler<LocalRemovidoEvent>, LocalEventHandler>();

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
