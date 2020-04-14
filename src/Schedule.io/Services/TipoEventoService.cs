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

        public void Gravar(TipoEvento tipoEvento)
        {
            var tipoEventoQuery = _tipoEventoRepository.Obter(tipoEvento.Id);
            if (tipoEventoQuery == null)
                Registrar(tipoEvento);
            else
                Atualizar(tipoEvento);

            ValidarComando();
        }

        public TipoEvento Obter(string tipoEventoId)
        {
            return _tipoEventoRepository.Obter(tipoEventoId);
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
            var tipoEvento = _tipoEventoRepository.Obter(tipoEventoId);
            if (tipoEvento == null)
            {
                _bus.PublicarNotificacao(new DomainNotification("", "Tipo Evento não encontrado!"));
                ValidarComando();
            }

            _tipoEventoRepository.Excluir(tipoEvento);

            if (Commit())
                _bus.PublicarEvento(new TipoEventoRemovidoEvent(tipoEvento.Id));

            ValidarComando();
        }

        #region Privados
        private void Registrar(TipoEvento tipoEvento)
        {
            _tipoEventoRepository.Adicionar(tipoEvento);

            if (Commit())
                _bus.PublicarEvento(new TipoEventoRegistradoEvent(tipoEvento.Id, tipoEvento.Nome, tipoEvento.Descricao));

            ValidarComando();
        }

        private void Atualizar(TipoEvento tipoEvento)
        {
            _tipoEventoRepository.Atualizar(tipoEvento);

            if (Commit())
                _bus.PublicarEvento(new TipoEventoAtualizadoEvent(tipoEvento.Id, tipoEvento.Nome, tipoEvento.Descricao));

            ValidarComando();
        }
        #endregion
    }
}
