using MediatR;
using Schedule.io.Core.Communication.Mediator;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Events.AgendaEvents;
using Schedule.io.Interfaces;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Interfaces.Services;
using Schedule.io.Models;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;

namespace Schedule.io.Services
{
    internal class AgendaService : ServiceBase, IAgendaService
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AgendaService(
            IAgendaRepository agendaRepository,
            IUsuarioRepository usuarioRepository,
            IMediatorHandler bus,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications) : base(bus, uow, notifications)
        {
            _usuarioRepository = usuarioRepository;
            _agendaRepository = agendaRepository;
        }

        public string Gravar(Agenda agenda)
        {
            VerificaUsuario(agenda);

            if (string.IsNullOrEmpty(agenda.Id))
                RegistrarAgenda(agenda);
            else
                AtualizarAgenda(agenda);

            return agenda.Id;
        }

        public Agenda Obter(string agendaId)
        {
            var agenda = RecuperaAgendaEValida(agendaId);

            ValidarComando();

            return agenda;
        }

        public IEnumerable<Agenda> Listar()
        {
            var agendas = _agendaRepository.Listar();
            foreach (var agenda in agendas)
            {
                yield return agenda;
            }
        }

        public IEnumerable<Agenda> Listar(string usuarioId)
        {
            var agendas = _agendaRepository.ListarAgendasPorUsuarioId(usuarioId);
            foreach (var agenda in agendas)
            {
                yield return agenda;
            }
        }

        public void Excluir(string agendaId)
        {
            var agenda = RecuperaAgendaEValida(agendaId);

            _agendaRepository.Excluir(agenda);

            if (Commit())
                _bus.PublicarEvento(new AgendaRemovidaEvent(agenda.Id)).Wait();

            ValidarComando();
        }

        #region Privados
        private void VerificaUsuario(Agenda agenda)
        {
            if (string.IsNullOrEmpty(agenda.UsuarioIdCriador))
                throw new ScheduleIoException(new List<string>() { "Id do dono da agenda não informado!" });

            if (!_usuarioRepository.VerificaSeUsuarioExiste(agenda.UsuarioIdCriador))
                throw new ScheduleIoException(new List<string>() { "Usuário não encontrado!" });
        }

        private Agenda RegistrarAgenda(Agenda agenda)
        {
            var novaAgenda = new Agenda(Guid.NewGuid().ToString(), agenda.Titulo);
            novaAgenda.DefinirDataCriacao();
            novaAgenda.DefinirDataAtualizacao();
            novaAgenda.DefinirUsuarioIdCriador(agenda.UsuarioIdCriador);
            novaAgenda.DefinirDescricao(agenda.Descricao);

            if (agenda.Publico)
                novaAgenda.TornarAgendaPublica();

            _agendaRepository.Adicionar(novaAgenda);

            if (Commit())
            {
                _bus.PublicarEvento(new AgendaRegistradaEvent(novaAgenda.Id, novaAgenda.UsuarioIdCriador, novaAgenda.Titulo, novaAgenda.Descricao, novaAgenda.Publico));

                RegistrarAgendaUsuario(novaAgenda);
            }

            ValidarComando();

            return novaAgenda;
        }

        private Agenda AtualizarAgenda(Agenda agenda)
        {
            var agendaAtualizar = _agendaRepository.Obter(agenda.Id);

            agendaAtualizar.DefinirDataAtualizacao();
            agendaAtualizar.DefinirTitulo(agenda.Titulo);
            agendaAtualizar.DefinirDescricao(agenda.Descricao);
            //agenda.DefinirUsuarioIdCriador(agendaAtualizar.UsuarioIdCriador);

            if (agenda.Publico)
                agendaAtualizar.TornarAgendaPublica();
            else
                agendaAtualizar.TornarAgendaPrivado();

            _agendaRepository.Atualizar(agendaAtualizar);

            if (Commit())
                _bus.PublicarEvento(new AgendaAtualizadaEvent(agendaAtualizar.Id, agendaAtualizar.UsuarioIdCriador, agendaAtualizar.Titulo, agendaAtualizar.Descricao, agendaAtualizar.Publico));

            RegistrarAgendaUsuario(agendaAtualizar);

            ValidarComando();

            return agendaAtualizar;
        }

        private void RegistrarAgendaUsuario(Agenda agenda)
        {
            if (_agendaRepository.ObterAgendaPorUsuarioId(agenda.Id, agenda.UsuarioIdCriador) == null)
                _agendaRepository.Gravar(new AgendaUsuario(agenda.Id, agenda.UsuarioIdCriador));
        }

        private Agenda RecuperaAgendaEValida(string agendaId)
        {
            var agenda = _agendaRepository.Obter(agendaId);

            if (agenda == null)
                throw new ScheduleIoException(new List<string>() { "Agenda não encontrada!" });

            return agenda;
        }
        #endregion
    }
}
