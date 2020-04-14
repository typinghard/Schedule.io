using Schedule.io.Interfaces.Services;
using Schedule.io.Interfaces;
using Schedule.io.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Core.Communication.Mediator;
using MediatR;
using Schedule.io.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Enums;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.ValueObjects;
using Schedule.io.Events.EventoAgendaEvents;

namespace Schedule.io.Services
{
    internal class EventoService : ServiceBase, IEventoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILocalRepository _localRepository;
        private readonly IEventoAgendaRepository _eventoAgendaRepository;

        public EventoService(IUsuarioRepository usuarioRepository,
                             ILocalRepository localRepository,
                             IEventoAgendaRepository eventoAgendaRepository,
                             IMediatorHandler bus,
                             IUnitOfWork uow,
                             INotificationHandler<DomainNotification> notifications) : base(bus, uow, notifications)
        {
            _usuarioRepository = usuarioRepository;
            _localRepository = localRepository;
            _eventoAgendaRepository = eventoAgendaRepository;
        }


        public void Gravar(Evento evento)
        {
            var eventoQuery = _eventoAgendaRepository.Obter(evento.Id);
            if (eventoQuery == null)
                Registrar(evento);
            else
                Atualizar(evento);

            ValidarComando();
        }

        public Evento Obter(string eventoId)
        {
            return _eventoAgendaRepository.Obter(eventoId);
        }

        public IEnumerable<Evento> Listar(string agendaId)
        {
            var eventos = _eventoAgendaRepository.ListarEventosDaAgenda(agendaId);
            foreach (var evento in eventos)
            {
                yield return evento;
            }
        }

        public IEnumerable<Evento> Listar(string agendaId, string usuarioId)
        {
            var eventos = _eventoAgendaRepository.ListarTodosEventosDoUsuario(agendaId, usuarioId);
            foreach (var evento in eventos)
            {
                yield return evento;
            }
        }

        public IEnumerable<Evento> Listar(string agendaId, DateTime dataInicial, DateTime dataFinal)
        {
            var eventos = _eventoAgendaRepository.ListarEventosPorPeriodo(agendaId, dataInicial, dataFinal);
            foreach (var evento in eventos)
            {
                yield return evento;
            }
        }

        public void Excluir(string eventoId)
        {
            var evento = _eventoAgendaRepository.Obter(eventoId);
            if (evento == null)
            {
                _bus.PublicarNotificacao(new DomainNotification("", "Evento não encontrado!"));
                ValidarComando();
            }

            _eventoAgendaRepository.Excluir(evento);

            if (Commit())
                _bus.PublicarEvento(new EventoAgendaRemovidoEvent(evento.Id));

            ValidarComando();
        }

        private void Registrar(Evento evento)
        {
            _eventoAgendaRepository.Adicionar(evento);

            if (Commit())
            {
                _bus.PublicarEvento(new EventoRegistradoEvent(evento.Id, evento.AgendaId, evento.UsuarioIdCriador, evento.IdentificadorExterno, evento.Titulo,
                                    evento.Descricao, evento.Convites, evento.LocalId, evento.DataInicio, evento.DataFinal,
                                    evento.DataLimiteConfirmacao, evento.QuantidadeMinimaDeUsuarios, evento.OcupaUsuario, evento.Publico,
                                    evento.IdTipoEvento, evento.Frequencia));

               // GravarConvites(evento);
            }

            ValidarComando();
        }

        private void Atualizar(Evento evento)
        {
            _eventoAgendaRepository.Atualizar(evento);

            if (Commit())
            {
                _bus.PublicarEvento(new EventoAtualizadoEvent(evento.Id, evento.AgendaId, evento.UsuarioIdCriador, evento.IdentificadorExterno, evento.Titulo,
                                    evento.Descricao, evento.Convites, evento.LocalId, evento.DataInicio, evento.DataFinal,
                                    evento.DataLimiteConfirmacao, evento.QuantidadeMinimaDeUsuarios, evento.OcupaUsuario, evento.Publico,
                                    evento.IdTipoEvento, evento.Frequencia));

               // GravarConvites(evento);
            }

            ValidarComando();
        }

        private void GravarConvites(Evento evento)
        {
            if (evento.Convites.Count == 0)
                return;

            //ExcluirConvites(evento);

            foreach (var convite in evento.Convites)
                _eventoAgendaRepository.AdicionarConvite(convite);
        }

        private void ExcluirConvites(Evento evento)
        {
            if (evento.Convites.Count == 0)
                _bus.PublicarNotificacao(new DomainNotification("", "Não há convites no evento!"));

            var listConvites = _eventoAgendaRepository.ListarConvites(evento.Id);

            foreach (var convite in listConvites)
                _eventoAgendaRepository.ExcluirConvite(convite);
        }
    }
}
