using Agenda.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Domain.EventHandlers
{
    public class ConviteEventHandler :
        INotificationHandler<ConviteRegistradoEvent>,
        INotificationHandler<ConviteAtualizadoEvent>,
        INotificationHandler<ConviteRemovidoEvent>
    {
        public Task Handle(ConviteRegistradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ConviteAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ConviteRemovidoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
