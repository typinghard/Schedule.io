using MediatR;
using Schedule.io.Core.Communication.Mediator;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Events.TipoEventoEvents;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Interfaces.Services;
using Schedule.io.Models.AggregatesRoots;
using System;
using System.Collections.Generic;

namespace Schedule.io.Services
{
    public class TipoEventoService : ServiceBase, ITipoEventoService
    {
        private readonly ITipoEventoRepository _tipoEventoRepository;

        public TipoEventoService(
            ITipoEventoRepository tipoEventoRepository,
            IMediatorHandler bus,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications
            ) : base(bus, uow, notifications)
        {
            _tipoEventoRepository = tipoEventoRepository;
        }

        public string Gravar(TipoEvento tipoEvento)
        {
            if (string.IsNullOrEmpty(tipoEvento.Id))
                RegistrarTipoEvento(tipoEvento);
            else
                AtualizarTipoEvento(tipoEvento);

            return tipoEvento.Id;
        }

        public TipoEvento Obter(string tipoEventoId)
        {
            var tipoEvento = RecuperaTipoEventoEValida(tipoEventoId);
            ValidarComando();
            return tipoEvento;
        }

        public IEnumerable<TipoEvento> Listar()
        {
            var tipoEventos = _tipoEventoRepository.Listar();
            foreach (var tipoevento in tipoEventos)
            {
                yield return tipoevento;
            }
        }

        public void Excluir(string tipoEventoId)
        {
            var tipoEvento = RecuperaTipoEventoEValida(tipoEventoId);

            _tipoEventoRepository.Excluir(tipoEvento);

            if (Commit())
                _bus.PublicarEvento(new TipoEventoRemovidoEvent(tipoEvento.Id));

            ValidarComando();
        }

        #region Privados
        private TipoEvento RegistrarTipoEvento(TipoEvento tipoEvento)
        {
            var novoTipoEvento = new TipoEvento(Guid.NewGuid().ToString(), tipoEvento.Nome, tipoEvento.Descricao);

            _tipoEventoRepository.Adicionar(novoTipoEvento);

            if (Commit())
                _bus.PublicarEvento(new TipoEventoRegistradoEvent(novoTipoEvento.Id, novoTipoEvento.Nome, novoTipoEvento.Descricao));

            ValidarComando();

            return novoTipoEvento;
        }

        private TipoEvento AtualizarTipoEvento(TipoEvento tipoEvento)
        {
            var atualizarTipoEvento = _tipoEventoRepository.Obter(tipoEvento.Id);
            atualizarTipoEvento.DefinirNome(tipoEvento.Nome);
            atualizarTipoEvento.DefinirDescricao(tipoEvento.Descricao);

            _tipoEventoRepository.Atualizar(atualizarTipoEvento);

            if (Commit())
                _bus.PublicarEvento(new TipoEventoAtualizadoEvent(atualizarTipoEvento.Id, atualizarTipoEvento.Nome, atualizarTipoEvento.Descricao));

            ValidarComando();

            return atualizarTipoEvento;
        }

        private TipoEvento RecuperaTipoEventoEValida(string tipoEventoId)
        {
            var tipoEvento = _tipoEventoRepository.Obter(tipoEventoId);

            if (tipoEvento == null)
                throw new ScheduleIoException(new List<string>() { "Tipo Evento não encontrado!" });

            return tipoEvento;
        } 
        #endregion
    }
}
