using Agenda.Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Domain.EventHandlers
{
    public class EventoAgendaEventHandler :
           INotificationHandler<EventoAgendaRegistradoEvent>,
          INotificationHandler<EventoAgendaAtualizadoEvent>,
          INotificationHandler<EventoAgendaRemovidoEvent>
    {
        public Task Handle(EventoAgendaRemovidoEvent notification, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(EventoAgendaRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(EventoAgendaAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }
    }
}
