using MediatR;
using Schedule.io.Core.Core.Communication.Mediator;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Services
{
    public abstract class ServiceBase
    {
        protected readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _bus;
        public ServiceBase(
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
        {
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public void ValidarComando()
        {
            if (_notifications.TemNotificacao())
                throw new ScheduleIoException(_notifications.ObterNotificacoes().Select(x => x.Key + ": " + x.Value).ToList());
        }

    }
}
