using MediatR;
using Schedule.io.Core.Communication.Mediator;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Interfaces.Repositories;
using System.Linq;

namespace Schedule.io.Services
{
    public abstract class ServiceBase
    {
        protected readonly DomainNotificationHandler _notifications;
        private readonly IUnitOfWork _uow;
        protected readonly IMediatorHandler _bus;
        public ServiceBase(
            IMediatorHandler bus,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications)
        {
            _bus = bus;
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public void ValidarComando()
        {
            if (_notifications.TemNotificacao())
                throw new ScheduleIoException(_notifications.ObterNotificacoes().Select(x => x.Key + ": " + x.Value).ToList());
        }


        public bool Commit()
        {
            if (_notifications.TemNotificacao()) return false;
            if (_uow.Commit()) return true;

            _bus.PublicarNotificacao(new DomainNotification("Commit", "Houve um problema durante a gravação dos dados."));
            return false;
        }

    }
}
