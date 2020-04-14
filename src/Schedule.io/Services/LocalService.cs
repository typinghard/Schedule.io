using MediatR;
using Schedule.io.Core.Communication.Mediator;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Interfaces.Services;
using System;
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

        public string Gravar(Local local)
        {
            if (string.IsNullOrEmpty(local.Id))
                RegistrarLocal(local);
            else
                AtualizarLocal(local);

            ValidarComando();
            return local.Id;
        }

        public Local Obter(string localId)
        {
            var local = RecuperaLocalEValida(localId);

            ValidarComando();

            return local;
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
            var local = RecuperaLocalEValida(localId);

            _localRepository.Excluir(local);

            if (Commit())
                _bus.PublicarEvento(new LocalRemovidoEvent(local.Id));

            ValidarComando();
        }

        #region Privados
        private Local RegistrarLocal(Local local)
        {
            var novoLocal = new Local(Guid.NewGuid().ToString(), local.Nome);
            novoLocal.DefinirDescricao(local.Descricao);
            novoLocal.DefinirIdentificadorExterno(local.IdentificadorExterno);
            novoLocal.DefinirLotacaoMaxima(local.LotacaoMaxima);

            if (local.Reserva)
                novoLocal.ReservarLocal();

            _localRepository.Adicionar(novoLocal);

            if (Commit())
                _bus.PublicarEvento(new LocalRegistradoEvent(novoLocal.Id, novoLocal.IdentificadorExterno, novoLocal.Nome, novoLocal.Descricao, novoLocal.Reserva, novoLocal.LotacaoMaxima));

            ValidarComando();

            return novoLocal;
        }

        private Local AtualizarLocal(Local local)
        {
            var atualizarLocal = _localRepository.Obter(local.Id);
            atualizarLocal.DefinirDescricao(local.Descricao);
            atualizarLocal.DefinirIdentificadorExterno(local.IdentificadorExterno);
            atualizarLocal.DefinirLotacaoMaxima(local.LotacaoMaxima);

            if (local.Reserva)
                atualizarLocal.ReservarLocal();
            else
                atualizarLocal.RemoverReservaLocal();

            _localRepository.Atualizar(atualizarLocal);

            if (Commit())
                _bus.PublicarEvento(new LocalAtualizadoEvent(atualizarLocal.Id, atualizarLocal.IdentificadorExterno, atualizarLocal.Nome, atualizarLocal.Descricao, atualizarLocal.Reserva, atualizarLocal.LotacaoMaxima));

            ValidarComando();

            return atualizarLocal;
        }

        private Local RecuperaLocalEValida(string localId)
        {
            var local = _localRepository.Obter(localId);

            if (local == null)
                throw new ScheduleIoException(new List<string>() { "Local não encontrado!" });

            return local;
        } 
        #endregion
    }
}
