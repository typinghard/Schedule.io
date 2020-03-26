using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleIo.Nuget.Services
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
