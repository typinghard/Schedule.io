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
    public class LocalAppService : ILocalAppService
    {
        private readonly IMapper _mapper;
        private readonly ILocalRepository _localRepository;
        private readonly IMediatorHandler Bus;

        public LocalAppService(IMapper mapper, 
                               ILocalRepository localRepository, 
                               IMediatorHandler bus)
        {
            _mapper = mapper;
            _localRepository = localRepository;
            Bus = bus;
        }

        public void Atualizar(AtualizarLocalViewModel localViewModel)
        {
            var atualizarCommand = _mapper.Map<AtualizarLocalCommand>(localViewModel);
            Bus.EnviarComando(atualizarCommand);
        }

        public DetalhesLocalViewModel ObterPorId(string id)
        {
            return _mapper.Map<DetalhesLocalViewModel>(_localRepository.ObterPorId(id));
        }

        public IEnumerable<DetalhesLocalViewModel> ObterTodosLocaisAtivos()
        {
            return _mapper.Map<IEnumerable<DetalhesLocalViewModel>>(_localRepository.ObterTodosAtivos());
        }

        public void Registrar(CriarLocalViewModel localViewModel)
        {
            var registrarCommand = _mapper.Map<RegistrarLocalCommand>(localViewModel);
            Bus.EnviarComando(registrarCommand);
        }

        public void Remover(string id)
        {
            var removerCommand = new RemoverLocalCommand(id);
            Bus.EnviarComando(removerCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
