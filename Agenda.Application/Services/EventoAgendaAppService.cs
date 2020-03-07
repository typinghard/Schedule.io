using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels;
using Agenda.Domain.Commands;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Interfaces;

namespace Agenda.Application.Services
{
    public class EventoAgendaAppService : IEventoAgendaAppService
    {

        private readonly IMapper _mapper;
        private readonly IEventoAgendaRepository _eventoAgendaRepository;
        private readonly IMediatorHandler Bus;

        public EventoAgendaAppService(IMapper mapper,
                                      IEventoAgendaRepository eventoAgendaRepository,
                                      IMediatorHandler bus)
        {
            _mapper = mapper;
            _eventoAgendaRepository = eventoAgendaRepository;
            Bus = bus;
        }

        public DetalhesEventoAgendaViewModel ObterPorId(Guid id)
        {
            return _mapper.Map<DetalhesEventoAgendaViewModel>(_eventoAgendaRepository.ObterPorId(id));
        }

        public IEnumerable<DetalhesEventoAgendaViewModel> ObterTodosEventosAgendaAtivos()
        {
            return _mapper.Map<IEnumerable<DetalhesEventoAgendaViewModel>>(_eventoAgendaRepository.ObterTodosAtivos());
        }

        public void Registrar(CriarEventoAgendaViewModel eventoViewModel)
        {
            var registrarCommand = _mapper.Map<RegistrarEventoAgendaCommand>(eventoViewModel);
            Bus.EnviarComando(registrarCommand);
        }

        public void Atualizar(AtualizarEventoAgendaViewModel eventoViewModel)
        {
            var atualizarCommand = _mapper.Map<AtualizarEventoAgendaCommand>(eventoViewModel);
            Bus.EnviarComando(atualizarCommand);
        }

        public void Remover(Guid id)
        {
            var removerCommand = new RemoverEventoAgendaCommand(id);
            Bus.EnviarComando(removerCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
