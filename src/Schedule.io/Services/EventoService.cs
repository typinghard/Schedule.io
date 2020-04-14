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

       
        public string Gravar(Evento evento)
        {
            if (string.IsNullOrEmpty(evento.Id))
                RegistrarEvento(evento);
            else
                AtualizarEvento(evento);

            ValidarComando();
            return evento.Id;
        }

        public Evento Obter(string eventoId)
        {
            var evento = RecuperaEventoEValida(eventoId);

            ValidarComando();

            return evento;
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
            var evento = RecuperaEventoEValida(eventoId);

            ExcluirConvites(evento);

            _eventoAgendaRepository.Excluir(evento);

            if (Commit())
                _bus.PublicarEvento(new EventoAgendaRemovidoEvent(evento.Id));

            ValidarComando();
        }

        private Evento RegistrarEvento(Evento evento)
        {
            var novoEvento = new Evento(Guid.NewGuid().ToString(), evento.AgendaId, evento.UsuarioIdCriador, evento.Titulo, evento.DataInicio, evento.IdTipoEvento);

            novoEvento.DefinirIdentificadorExterno(evento.IdentificadorExterno);
            novoEvento.DefinirDescricao(evento.Descricao);
            novoEvento.DefinirLocal(evento.LocalId);
            novoEvento.DefinirDatas(evento.DataInicio, evento.DataFinal);
            novoEvento.DefinirDataLimiteConfirmacao(evento.DataLimiteConfirmacao.GetValueOrDefault());
            novoEvento.DefinirQuantidadeMinimaDeUsuarios(evento.QuantidadeMinimaDeUsuarios);
            novoEvento.DefinirFrequencia(evento.Frequencia);

            foreach (var convite in evento.Convites)
                novoEvento.AdicionarConvite(convite);

            if (evento.OcupaUsuario)
                novoEvento.OcuparUsuario();

            if (evento.Publico)
                novoEvento.TornarEventoPublico();

            _eventoAgendaRepository.Adicionar(novoEvento);

            if (Commit())
            {
                _bus.PublicarEvento(new EventoRegistradoEvent(novoEvento.Id, novoEvento.AgendaId, novoEvento.UsuarioIdCriador, novoEvento.IdentificadorExterno, novoEvento.Titulo,
                                    novoEvento.Descricao, novoEvento.Convites, novoEvento.LocalId, novoEvento.DataInicio, novoEvento.DataFinal,
                                    novoEvento.DataLimiteConfirmacao, novoEvento.QuantidadeMinimaDeUsuarios, novoEvento.OcupaUsuario, novoEvento.Publico,
                                    novoEvento.IdTipoEvento, novoEvento.Frequencia));

                GravarConvites(novoEvento);
            }

            ValidarComando();

            return novoEvento;
        }

        private Evento AtualizarEvento(Evento evento)
        {
            var atualizarEvento = RecuperaEventoEValida(evento.Id);
            atualizarEvento.DefinirTipoEvento(evento.IdTipoEvento);
            atualizarEvento.DefinirIdentificadorExterno(evento.IdentificadorExterno);
            atualizarEvento.DefinirTitulo(evento.Titulo);
            atualizarEvento.DefinirDescricao(evento.Descricao);
            atualizarEvento.DefinirLocal(evento.LocalId);
            atualizarEvento.DefinirDatas(evento.DataInicio, evento.DataFinal.GetValueOrDefault());
            atualizarEvento.DefinirDataLimiteConfirmacao(evento.DataLimiteConfirmacao.GetValueOrDefault());
            atualizarEvento.DefinirQuantidadeMinimaDeUsuarios(evento.QuantidadeMinimaDeUsuarios);
            atualizarEvento.DefinirFrequencia(evento.Frequencia);

            ExcluirConvites(atualizarEvento);
            atualizarEvento.LimparConvites();
            foreach (var convite in evento.Convites)
                atualizarEvento.AdicionarConvite(convite);

            if (evento.OcupaUsuario)
                atualizarEvento.OcuparUsuario();
            else
                atualizarEvento.DesocuparUsuario();

            if (evento.Publico)
                atualizarEvento.TornarEventoPublico();
            else
                atualizarEvento.TornarEventoPrivado();

            _eventoAgendaRepository.Atualizar(atualizarEvento);

            if (Commit())
            {
                _bus.PublicarEvento(new EventoAtualizadoEvent(atualizarEvento.Id, atualizarEvento.AgendaId, atualizarEvento.UsuarioIdCriador, atualizarEvento.IdentificadorExterno, atualizarEvento.Titulo,
                                    atualizarEvento.Descricao, atualizarEvento.Convites, atualizarEvento.LocalId, atualizarEvento.DataInicio, atualizarEvento.DataFinal,
                                    atualizarEvento.DataLimiteConfirmacao, atualizarEvento.QuantidadeMinimaDeUsuarios, atualizarEvento.OcupaUsuario, atualizarEvento.Publico,
                                    atualizarEvento.IdTipoEvento, atualizarEvento.Frequencia));

                GravarConvites(evento);
            }

            ValidarComando();

            return atualizarEvento;
        }

        private void GravarConvites(Evento evento)
        {
            if (evento.Convites.Count == 0)
                return;

            ExcluirConvites(evento);

            foreach (var convite in evento.Convites)
                _eventoAgendaRepository.AdicionarConvite(convite);
        }

        private void ExcluirConvites(Evento evento)
        {
            if (evento.Convites.Count == 0)
                return;

            var listConvites = _eventoAgendaRepository.ListarConvites(evento.Id);

            foreach (var convite in listConvites)
                _eventoAgendaRepository.ExcluirConvite(convite);
        }

        private Evento RecuperaEventoEValida(string eventoId)
        {
            var evento = _eventoAgendaRepository.Obter(eventoId);

            if (evento == null)
                throw new ScheduleIoException(new List<string>() { "Evento não encontrado!" });

            return evento;
        }
    }
}
