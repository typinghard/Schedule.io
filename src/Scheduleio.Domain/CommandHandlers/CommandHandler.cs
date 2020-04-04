using MediatR;
using Schedule.io.Core.Core.Communication.Mediator;
using Schedule.io.Core.Core.Messages;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Core.Interfaces;

namespace Schedule.io.Core.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        public bool Commit()
        {
            if (_notifications.TemNotificacao()) return false;
            if (_uow.Commit()) return true;

            _bus.PublicarNotificacao(new DomainNotification("Commit", "We had a problem during saving your data."));
            return false;
        }
    }
}