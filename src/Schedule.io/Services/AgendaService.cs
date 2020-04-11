using MediatR;
using Schedule.io.Core.Commands.AgendaCommands;
using Schedule.io.Core.Commands.AgendaUsuarioCommands;
using Schedule.io.Core.Core.Communication.Mediator;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Core.Interfaces;
using Schedule.io.Interfaces;
using Schedule.io.Models;
using System;
using System.Collections.Generic;

namespace Schedule.io.Services
{
    internal class AgendaService : ServiceBase, IAgendaService
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMediatorHandler _bus;
        public AgendaService(
            IAgendaRepository agendaRepository,
            IUsuarioService usuarioService,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _bus = bus;
            _usuarioService = usuarioService;
            _agendaRepository = agendaRepository;
        }

        public string Gravar(Agenda agenda)
        {
            if (agenda.Usuario != null)
                agenda.Usuario.Id = _usuarioService.Gravar(agenda.Usuario);

            if (string.IsNullOrEmpty(agenda.Id))
            {
                agenda.Id = Guid.NewGuid().ToString();
                _bus.EnviarComando(new RegistrarAgendaCommand(agenda.Id, agenda.Titulo, agenda.Descricao, agenda.Publico)).Wait();
            }
            else
                _bus.EnviarComando(new AtualizarAgendaCommand(agenda.Id, agenda.Titulo, agenda.Descricao, agenda.Publico)).Wait();

            RegistraAgendaUsuario(agenda);

            ValidarComando();
            return agenda.Id;
        }

        private void RegistraAgendaUsuario(Models.Agenda agenda)
        {
            if (agenda.Usuario != null)
            {
                if (_agendaRepository.ObterAgendaPorUsuarioId(agenda.Id, agenda.Usuario.Id) == null)
                    _bus.EnviarComando(new RegistrarAgendaUsuarioCommand(agenda.Id, agenda.Usuario.Id)).Wait();
            }
        }

        public bool Inativar(string agendaId)
        {
            var agenda = _agendaRepository.ObterPorId(agendaId);
            _agendaRepository.Remover(agenda);
            ValidarComando();
            return true;
        }

        public Agenda Obter(string agendaId)
        {
            var agenda = _agendaRepository.ObterPorId(agendaId);

            if (agenda == null)
            {
                throw new ScheduleIoException(new List<string>() { "Agenda não encontrada!" });
            }

            return new Models.Agenda()
            {
                Id = agenda.Id,
                CriadoAs = agenda.CriadoAs,
                AtualizadoAs = agenda.AtualizadoAs,
                Titulo = agenda.Titulo,
                Descricao = agenda.Descricao,
                Publico = agenda.Publico
            };
        }

        public IEnumerable<Models.Agenda> ObterTodas()
        {
            var agendas = _agendaRepository.ObterTodosAtivos();
            foreach (var agenda in agendas)
            {
                yield return new Models.Agenda()
                {
                    Id = agenda.Id,
                    CriadoAs = agenda.CriadoAs,
                    AtualizadoAs = agenda.AtualizadoAs,
                    Titulo = agenda.Titulo,
                    Descricao = agenda.Descricao,
                    Publico = agenda.Publico
                };
            }
        }
    }
}
