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
    public class EventoUsuarioAppService : IEventoUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IConviteRepository _conviteRepository;
        private readonly IMediatorHandler Bus;

        public EventoUsuarioAppService(IMapper mapper,
                                       IConviteRepository conviteRepository, 
                                       IMediatorHandler bus)
        {
            _mapper = mapper;
            _conviteRepository = conviteRepository;
            Bus = bus;
        }

        public void Atualizar(AtualizarEventoUsuarioViewModel eventoUsuarioViewModel)
        {
            var atualizarCommand = _mapper.Map<AtualizarEventoUsuarioCommand>(eventoUsuarioViewModel);
            Bus.EnviarComando(atualizarCommand);
        }

        public DetalhesEventoUsuarioViewModel ObterPorId(string id)
        {
            return _mapper.Map<DetalhesEventoUsuarioViewModel>(_conviteRepository.ObterPorId(id));
        }

        public IEnumerable<DetalhesEventoUsuarioViewModel> ObterTodosEventosUsuarioAtivos()
        {
            return _mapper.Map<IEnumerable<DetalhesEventoUsuarioViewModel>>(_conviteRepository.ObterTodosAtivos());
        }

        public void Registrar(CriarEventoUsuarioViewModel eventoUsuarioViewModel)
        {
            var registrarCommand = _mapper.Map<RegistrarConviteCommand>(eventoUsuarioViewModel);
            Bus.EnviarComando(registrarCommand);
        }

        public void Remover(string id)
        {
            var removerCommand = new RemoverConviteCommand(id);
            Bus.EnviarComando(removerCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
