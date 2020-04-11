using MediatR;
using Schedule.io.Core.Core.Communication.Mediator;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Services
{
    public class TipoEventoService : ServiceBase, ITipoEventoService
    {
        public TipoEventoService(
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications
            ) : base(bus, notifications)
        {
        }

    }
}
