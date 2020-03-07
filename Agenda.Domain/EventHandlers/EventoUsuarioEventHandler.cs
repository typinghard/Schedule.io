using Agenda.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Domain.EventHandlers
{
    public class EventoUsuarioEventHandler :
        INotificationHandler<EventoUsuarioRegistradoEvent>,
        INotificationHandler<EventoUsuarioAtualizadoEvent>,
        INotificationHandler<EventoUsuarioRemovidoEvent>
    {
        public Task Handle(EventoUsuarioRegistradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(EventoUsuarioAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(EventoUsuarioRemovidoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
