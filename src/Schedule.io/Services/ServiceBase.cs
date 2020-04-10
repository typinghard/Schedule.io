using MediatR;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Services
{
    public class ServiceBase
    {
        protected readonly DomainNotificationHandler _notifications;
        public ServiceBase(
            INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        public void ValidarComando()
        {
            if (_notifications.TemNotificacao())
                throw new ScheduleIoException(_notifications.ObterNotificacoes().Select(x => x.Key + ": " + x.Value).ToList());
        }

    }
}
