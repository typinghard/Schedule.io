using Agenda.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Domain.EventHandlers
{
    public class AgendaUsuarioEventHandler :
        INotificationHandler<AgendaUsuarioRegistradoEvent>,
        INotificationHandler<AgendaUsuarioAtualizadoEvent>,
        INotificationHandler<AgendaUsuarioRemovidoEvent>
    {
        public Task Handle(AgendaUsuarioRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // Send some notification e-mail
            return Task.CompletedTask;
        }

        public Task Handle(AgendaUsuarioAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            // Send some notification e-mail
            return Task.CompletedTask;
        }

        public Task Handle(AgendaUsuarioRemovidoEvent notification, CancellationToken cancellationToken)
        {
            // Send some notification e-mail
            return Task.CompletedTask;
        }
    }
}
