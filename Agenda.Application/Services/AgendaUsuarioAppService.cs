using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Commands;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Application.Services
{
    public class AgendaUsuarioAppService : IAgendaUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IAgendaUsuarioRepository _agendaUsuarioRepository;
        private readonly IMediatorHandler Bus;

        public AgendaUsuarioAppService(IMapper mapper, 
                                       IAgendaUsuarioRepository agendaUsuarioRepository,
                                       IMediatorHandler bus)
        {
            _mapper = mapper;
            _agendaUsuarioRepository = agendaUsuarioRepository;
            Bus = bus;
        }

        public void Atualizar(AtualizarAgendaUsuarioViewModel agendaUsuarioViewModel)
        {
            var atualizarCommand = _mapper.Map<AtualizarAgendaUsuarioCommand>(agendaUsuarioViewModel);
            Bus.EnviarComando(atualizarCommand);
        }



        public DetalhesAgendaUsuarioViewModel ObterPorId(Guid id)
        {
            return _mapper.Map<DetalhesAgendaUsuarioViewModel>(_agendaUsuarioRepository.ObterPorId(id));
        }

        public IEnumerable<DetalhesAgendaUsuarioViewModel> ObterTodasAtivos()
        {
            return _mapper.Map<IEnumerable<DetalhesAgendaUsuarioViewModel>>(_agendaUsuarioRepository.ObterTodosAtivos());
        }

        public void Registrar(CriarAgendaUsuarioViewModel agendaUsuarioViewModel)
        {
            var registrarCommand = _mapper.Map<RegistrarAgendaUsuarioCommand>(agendaUsuarioViewModel);
            Bus.EnviarComando(registrarCommand);
        }

        public void Remover(Guid id)
        {
            var removerCommand = new RemoverAgendaUsuarioCommand(id);
            Bus.EnviarComando(removerCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
