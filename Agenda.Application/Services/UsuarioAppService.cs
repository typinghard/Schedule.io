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
    public class UsuarioAppService : IUsuarioAppService
    {

        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMediatorHandler Bus;

        public UsuarioAppService(IMapper mapper,
                                 IUsuarioRepository usuarioRepository,
                                 IMediatorHandler bus)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            Bus = bus;
        }

        public void Atualizar(AtualizarUsuarioViewModel usuarioViewModel)
        {
            var atualizarCommand = _mapper.Map<AtualizarUsuarioCommand>(usuarioViewModel);
            Bus.EnviarComando(atualizarCommand);
        }

        public DetalhesUsuarioViewModel ObterPorId(string id)
        {
            return _mapper.Map<DetalhesUsuarioViewModel>(_usuarioRepository.ObterPorId(id));
        }

        public IEnumerable<DetalhesUsuarioViewModel> ObterTodosUsuariosAtivos()
        {
            return _mapper.Map<IEnumerable<DetalhesUsuarioViewModel>>(_usuarioRepository.ObterTodosAtivos());
        }

        public void Registrar(CriarUsuarioViewModel usuarioViewModel)
        {
            var registrarCommand = _mapper.Map<RegistrarUsuarioCommand>(usuarioViewModel);
            Bus.EnviarComando(registrarCommand);
        }

        public void Remover(string id)
        {
            var removerCommand = new RemoverUsuarioCommand(id);
            Bus.EnviarComando(removerCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
