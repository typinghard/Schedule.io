using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels;
using Agenda.Domain.Commands;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Application.Services
{
    public class AgendaAppService : IAgendaAppService
    {
        private readonly IMapper _mapper;
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMediatorHandler Bus;

        public AgendaAppService(IMapper mapper, 
                                IAgendaRepository agendaRepository, 
                                IMediatorHandler bus)
        {
            _mapper = mapper;
            _agendaRepository = agendaRepository;
            Bus = bus;
        }

        public void Atualizar(AtualizarAgendaViewModel agendaViewModel)
        {
            var atualizarCommand = _mapper.Map<AtualizarAgendaCommand>(agendaViewModel);
            Bus.EnviarComando(atualizarCommand);
        }

        public DetalhesAgendaViewModel ObterPorId(string id)
        {
            return _mapper.Map<DetalhesAgendaViewModel>(_agendaRepository.ObterPorId(id));
        }

        public IEnumerable<DetalhesAgendaViewModel> ObterTodasAgendasAtivas()
        {
            return _mapper.Map<IEnumerable<DetalhesAgendaViewModel>>(_agendaRepository.ObterTodosAtivos());
        }

        public void Registrar(CriarAgendaViewModel agendaViewModel)
        {
            var registrarCommand = _mapper.Map<RegistrarAgendaCommand>(agendaViewModel);
            Bus.EnviarComando(registrarCommand);
        }

        public void Remover(string id)
        {
            var removerCommand = new RemoverAgendaCommand(id);
            Bus.EnviarComando(removerCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
