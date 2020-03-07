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
        private readonly IEventoUsuarioRepository _eventoUsuarioRepository;
        private readonly IMediatorHandler Bus;

        public EventoUsuarioAppService(IMapper mapper, 
                                       IEventoUsuarioRepository eventoUsuarioRepository, 
                                       IMediatorHandler bus)
        {
            _mapper = mapper;
            _eventoUsuarioRepository = eventoUsuarioRepository;
            Bus = bus;
        }

        public void Atualizar(AtualizarEventoUsuarioViewModel eventoUsuarioViewModel)
        {
            var atualizarCommand = _mapper.Map<AtualizarEventoUsuarioCommand>(eventoUsuarioViewModel);
            Bus.EnviarComando(atualizarCommand);
        }

        public DetalhesEventoUsuarioViewModel ObterPorId(Guid id)
        {
            return _mapper.Map<DetalhesEventoUsuarioViewModel>(_eventoUsuarioRepository.ObterPorId(id));
        }

        public IEnumerable<DetalhesEventoUsuarioViewModel> ObterTodosEventosUsuarioAtivos()
        {
            return _mapper.Map<IEnumerable<DetalhesEventoUsuarioViewModel>>(_eventoUsuarioRepository.ObterTodosAtivos());
        }

        public void Registrar(CriarEventoUsuarioViewModel eventoUsuarioViewModel)
        {
            var registrarCommand = _mapper.Map<RegistrarEventoUsuarioCommand>(eventoUsuarioViewModel);
            Bus.EnviarComando(registrarCommand);
        }

        public void Remover(Guid id)
        {
            var removerCommand = new RemoverEventoUsuarioCommand(id);
            Bus.EnviarComando(removerCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
