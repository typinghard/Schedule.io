using MediatR;
using Schedule.io.Events.AgendaEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.io.Events.Handlers
{
    public class TesteHandler1 : INotificationHandler<AgendaAtualizadaEvent>
    {
        public Task Handle(AgendaAtualizadaEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler1");
            throw new NotImplementedException();
            //return Task.CompletedTask;
        }
    }
    public class TesteHandler2 : INotificationHandler<AgendaAtualizadaEvent>
    {
        public Task Handle(AgendaAtualizadaEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler2");
            throw new NotImplementedException();
            //return Task.CompletedTask;
        }
    }
}
