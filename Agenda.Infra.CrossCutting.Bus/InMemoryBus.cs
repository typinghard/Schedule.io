using System.Threading.Tasks;
using Agenda.Domain.Core.Bus;
using Agenda.Domain.Core.Messages;
using Agenda.Domain.Core.Messages.CommonMessages.DomainEvents;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;
using MediatR;


namespace Agenda.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        //private readonly IEventSourcingRepository _eventSourcingRepository;

        public InMemoryBus(/*IEventStore eventStore,*/ IMediator mediator)
        {
            //  _eventStore = eventStore;
            _mediator = mediator;
        }

        public Task<bool> EnviarComando<T>(T comando) where T : Command
        {
            throw new System.NotImplementedException();
            //return await _mediator.Send(comando);
        }

        public Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent
        {
            throw new System.NotImplementedException();
            //await _mediator.Publish(notificacao);
        }

        public Task PublicarEvento<T>(T evento) where T : Event
        {
            throw new System.NotImplementedException();
            //await _mediator.Publish(evento);
            //_eventSourcingRepository.SalvarEvento(evento);
        }

        public Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
        {
            throw new System.NotImplementedException();
            //await _mediator.Publish(notificacao);
        }
    }
}
