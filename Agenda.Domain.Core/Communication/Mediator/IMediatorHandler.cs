﻿using System.Threading.Tasks;
using Agenda.Domain.Core.Messages;
using Agenda.Domain.Core.Messages.CommonMessages.DomainEvents;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;

namespace Agenda.Domain.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<bool> EnviarComando<T>(T comando) where T : Command;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
        Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent;
    }
}
