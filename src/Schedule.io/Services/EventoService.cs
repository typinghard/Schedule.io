using Schedule.io.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Core.Communication.Mediator;
using MediatR;
using Schedule.io.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Enums;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.ValueObjects;
using Schedule.io.Events.EventoAgendaEvents;
using Schedule.io.Core.Helpers;
using Schedule.io.Configs;

namespace Schedule.io.Services
{
    internal class EventoService : ServiceBase, IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly ILocalRepository _localRepository;

        public EventoService(IEventoRepository eventoRepository,
                             ILocalRepository localRepository,
                             IMediatorHandler bus,
                             IUnitOfWork uow,
                             INotificationHandler<DomainNotification> notifications) : base(bus, uow, notifications)
        {
            _eventoRepository = eventoRepository;
            _localRepository = localRepository;
        }


        public void Gravar(Evento evento)
        {
            var eventoQuery = _eventoRepository.Obter(evento.Id);
            if (eventoQuery == null)
                Registrar(evento);
            else
                Atualizar(evento);

            ValidarComando();
        }

        public Evento Obter(string eventoId)
        {
            return _eventoRepository.Obter(eventoId);
        }

        public IEnumerable<Evento> Listar(string agendaId)
        {
            var eventos = _eventoRepository.Listar(agendaId);
            foreach (var evento in eventos)
            {
                yield return evento;
            }
        }

        public IEnumerable<Evento> Listar(string agendaId, string usuarioId)
        {
            var eventos = _eventoRepository.Listar(agendaId, usuarioId);
            foreach (var evento in eventos)
            {
                yield return evento;
            }
        }

        public IEnumerable<Evento> Listar(string agendaId, DateTime dataInicial, DateTime dataFinal)
        {
            var eventos = _eventoRepository.Listar(agendaId, dataInicial, dataFinal);
            foreach (var evento in eventos)
            {
                yield return evento;
            }
        }

        public void Excluir(string eventoId)
        {
            var evento = _eventoRepository.Obter(eventoId);
            if (evento == null)
            {
                _bus.PublicarNotificacao(new DomainNotification("", "Evento não encontrado!"));
                ValidarComando();
            }

            _eventoRepository.Excluir(evento);

            if (Commit())
                _bus.PublicarEvento(new EventoRemovidoEvent(evento.Id));

            ValidarComando();
        }

        private void Registrar(Evento evento)
        {
            Validar(evento);

            _eventoRepository.Adicionar(evento);

            if (Commit())
            {
                _bus.PublicarEvento(new EventoRegistradoEvent(evento.Id, evento.AgendaId, evento.UsuarioIdCriador, evento.IdentificadorExterno, evento.Titulo,
                                    evento.Descricao, evento.Convites, evento.LocalId, evento.DataInicio, evento.DataFinal,
                                    evento.DataLimiteConfirmacao, evento.QuantidadeMinimaDeUsuarios, evento.OcupaUsuario, evento.Publico,
                                    evento.IdTipoEvento, evento.Frequencia));
            }
        }

        private void Atualizar(Evento evento)
        {
            Validar(evento);

            _eventoRepository.Atualizar(evento);

            if (Commit())
            {
                _bus.PublicarEvento(new EventoAtualizadoEvent(evento.Id, evento.AgendaId, evento.UsuarioIdCriador, evento.IdentificadorExterno, evento.Titulo,
                                    evento.Descricao, evento.Convites, evento.LocalId, evento.DataInicio, evento.DataFinal,
                                    evento.DataLimiteConfirmacao, evento.QuantidadeMinimaDeUsuarios, evento.OcupaUsuario, evento.Publico,
                                    evento.IdTipoEvento, evento.Frequencia));
            }
        }

        private void Validar(Evento evento)
        {
            if(ScheduleIoConfigurationHelper.BloquearEventosOcupadosNoMesmoHorario)
                ValidarEventosOcupadoNoMesmoHorario(evento);

            if (ScheduleIoConfigurationHelper.ValidarLimiteDeQuantidadePorLocal)
                ValidaQuantidadeUsuarioReferenteAoLocal(evento);
        }

        private void ValidaQuantidadeUsuarioReferenteAoLocal(Evento evento)
        {
            if (!evento.LocalId.EhVazio() && evento.QuantidadeMinimaDeUsuarios > 0)
            {
                var local = _localRepository.Obter(evento.LocalId);

                if (evento.QuantidadeMinimaDeUsuarios > local.LotacaoMaxima)
                    _bus.PublicarNotificacao(new DomainNotification("Validação Evento", "Quantidade mínima de usuários não pode ser maior que a lotação máxima do local."));
            }
        }

        private void ValidarEventosOcupadoNoMesmoHorario(Evento evento)
        {
            var eventos = _eventoRepository.Listar(evento.AgendaId, evento.UsuarioIdCriador);

            if (!eventos.Any())
                return;

            if (eventos.Any(x => x.DataInicio == evento.DataInicio && (x.OcupaUsuario && evento.OcupaUsuario)))
                _bus.PublicarNotificacao(new DomainNotification("Validação Evento", "Usuario não pode ter dois ou mais eventos no mesmo horário marcados como ocupado."));
        }
    }
}
