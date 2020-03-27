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
    public class ConviteAppService : IConviteAppService
    {
        private readonly IMapper _mapper;
        private readonly IConviteRepository _conviteRepository;
        private readonly IMediatorHandler Bus;

        public ConviteAppService(IMapper mapper, 
                                       IConviteRepository conviteRepository, 
                                       IMediatorHandler bus)
        {
            _mapper = mapper;
            _conviteRepository = conviteRepository;
            Bus = bus;
        }

        public void Atualizar(AtualizarConviteViewModel eventoUsuarioViewModel)
        {
            var atualizarCommand = _mapper.Map<AtualizarConviteCommand>(eventoUsuarioViewModel);
            Bus.EnviarComando(atualizarCommand);
        }

        public DetalhesConviteViewModel ObterPorId(string id)
        {
            return _mapper.Map<DetalhesConviteViewModel>(_conviteRepository.ObterPorId(id));
        }

        public IEnumerable<DetalhesConviteViewModel> ObterTodosConvitesAtivos()
        {
            return _mapper.Map<IEnumerable<DetalhesConviteViewModel>>(_conviteRepository.ObterTodosAtivos());
        }

        public void Registrar(CriarConviteViewModel eventoUsuarioViewModel)
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
