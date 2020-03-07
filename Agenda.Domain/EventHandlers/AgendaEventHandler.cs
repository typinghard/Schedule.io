using Agenda.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Domain.EventHandlers
{
    public class AgendaEventHandler :
        INotificationHandler<AgendaRegistradaEvent>,
        INotificationHandler<AgendaAtualizadaEvent>,
        INotificationHandler<AgendaRemovidaEvent>
    {
        public Task Handle(AgendaRegistradaEvent notification, CancellationToken cancellationToken)
        {
            // Send some notification e-mail
            return Task.CompletedTask;
        }

        public Task Handle(AgendaAtualizadaEvent notification, CancellationToken cancellationToken)
        {
            // Send some notification e-mail
            return Task.CompletedTask;
        }

        public Task Handle(AgendaRemovidaEvent notification, CancellationToken cancellationToken)
        {
            // Send some notification e-mail
            return Task.CompletedTask;
        }
    }
}
