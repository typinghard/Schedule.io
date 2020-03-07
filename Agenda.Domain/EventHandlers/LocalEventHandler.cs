using Agenda.Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Domain.EventHandlers
{
    public class LocalEventHandler :
        INotificationHandler<LocalRegistradoEvent>,
        INotificationHandler<LocalAtualizadoEvent>,
        INotificationHandler<LocalRemovidoEvent>
    {
        public Task Handle(LocalRegistradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(LocalAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(LocalRemovidoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
