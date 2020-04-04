﻿using Schedule.io.Core.Core.Messages;
using Schedule.io.Core.Core.Messages.CommonMessages.DomainEvents;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace Schedule.io.Core.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<bool> EnviarComando<T>(T comando) where T : Command;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
        Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent;
    }
}