using MediatR;
using Schedule.io.Core.Commands.Agenda;
using Schedule.io.Core.Core.Communication.Mediator;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Core.Interfaces;
using Schedule.io.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schedule.io.Services
{
    internal class AgendaService : ServiceBase, IAgendaService 
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMediatorHandler _bus;
        public AgendaService(
            IAgendaRepository agendaRepository, 
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _bus = bus;
            _agendaRepository = agendaRepository;
        }

        public string Criar(Models.Agenda agenda)
        {
            var idAgenda = Guid.NewGuid().ToString();
            _bus.EnviarComando(new RegistrarAgendaCommand(idAgenda, "Primeiro teste", "Foi slc", true)).Wait();
            ValidarComando();
            return idAgenda;
        }

        public void Editar(Models.Agenda agenda)
        {
            throw new NotImplementedException();
        }

        public void Excluir(Models.Agenda agenda)
        {
            throw new NotImplementedException();
        }

        public Models.Agenda Obter(string agendaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Agenda> ObterTodas()
        {
            var agendas = _agendaRepository.ObterTodosAtivos();
            foreach(var agenda in agendas) 
            {
                yield return new Models.Agenda()
                {
                    Id = agenda.Id
                };
            }
        }
    }
}
