using Agenda.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Domain.EventHandlers
{
    public class UsuarioEventHandler :
         INotificationHandler<UsuarioRegistradoEvent>,
         INotificationHandler<UsuarioAtualizadoEvent>,
         INotificationHandler<UsuarioRemovidoEvent>
    {
        public Task Handle(UsuarioRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // Send some notification e-mail
            return Task.CompletedTask;
        }

        public Task Handle(UsuarioAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            // Send some notification e-mail
            return Task.CompletedTask;
        }

        public Task Handle(UsuarioRemovidoEvent notification, CancellationToken cancellationToken)
        {
            // Send some notification e-mail
            return Task.CompletedTask;
        }
    }
}
