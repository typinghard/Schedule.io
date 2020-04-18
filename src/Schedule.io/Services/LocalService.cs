using MediatR;
using Schedule.io.Core.Communication.Mediator;
using Schedule.io.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Interfaces.Services;
using System.Collections.Generic;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Events.LocalEvents;

namespace Schedule.io.Services
{
    internal class LocalService : ServiceBase, ILocalService
    {
        private readonly ILocalRepository _localRepository;

        public LocalService(ILocalRepository localRepository,
            IMediatorHandler bus,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications) : base(bus, uow, notifications)
        {
            _localRepository = localRepository;
        }

        public void Gravar(Local local)
        {
            var localQuery = _localRepository.Obter(local.Id);
            if (localQuery == null)
                Registrar(local);
            else
                Atualizar(local);

            ValidarComando();
        }

        public Local Obter(string localId)
        {
            return _localRepository.Obter(localId);
        }

        public IEnumerable<Local> Listar()
        {
            var locais = _localRepository.Listar();
            foreach (var local in locais)
            {
                yield return local;
            }
        }

        public void Excluir(string localId)
        {
            var local = _localRepository.Obter(localId);
            if (local == null)
            {
                _bus.PublicarNotificacao(new DomainNotification("", "Local não encontrado!"));
                ValidarComando();
            }

            _localRepository.Excluir(local);

            if (Commit())
                _bus.PublicarEvento(new LocalRemovidoEvent(local.Id));

            ValidarComando();
        }

        #region Privados
        private void Registrar(Local local)
        {
            _localRepository.Adicionar(local);

            if (Commit())
                _bus.PublicarEvento(new LocalRegistradoEvent(local.Id, local.IdentificadorExterno, local.Nome, local.Descricao, local.Reserva, local.LotacaoMaxima));

            ValidarComando();
        }

        private void Atualizar(Local local)
        {
            _localRepository.Atualizar(local);

            if (Commit())
                _bus.PublicarEvento(new LocalAtualizadoEvent(local.Id, local.IdentificadorExterno, local.Nome, local.Descricao, local.Reserva, local.LotacaoMaxima));

            ValidarComando();
        }
        #endregion
    }
}
