using Agenda.Domain.Commands;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;
using Agenda.Domain.Events;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Agenda.Domain.Core.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Domain.CommandHandlers
{
    public class LocalCommandHandler : CommandHandler,
        IRequestHandler<RegistrarLocalCommand, bool>,
        IRequestHandler<AtualizarLocalCommand, bool>,
        IRequestHandler<RemoverLocalCommand, bool>
    {
        private readonly ILocalRepository _localRepository;
        private readonly IMediatorHandler Bus;
        public LocalCommandHandler(ILocalRepository localRepository,
        IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            this._localRepository = localRepository;
            this.Bus = bus;
        }

        public Task<bool> Handle(RegistrarLocalCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            Local local = new Local(message.NomeLocal);

            if (!string.IsNullOrEmpty(message.IdentificadorExterno))
                local.DefinirIdentificadorExterno(message.IdentificadorExterno);

            if (!string.IsNullOrEmpty(message.Descricao))
                local.DefinirDescricao(message.Descricao);

            if (message.ReservaLocal)
                local.ReservarLocal();
            else
                local.RemoverReservaLocal();

            if (message.LotacaoMaxima > 0)
                local.DefinirLotacaoMaxima(message.LotacaoMaxima);

            _localRepository.Adicionar(local);

            if (Commit())
            {
                Bus.PublicarEvento(new LocalRegistradoEvent(local.Id, local.IdentificadorExterno, local.NomeLocal, local.Descricao, local.ReservaLocal, local.LotacaoMaxima));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(AtualizarLocalCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            Local local = _localRepository.ObterPorId(message.Id);
            if (local == null)
            {
                Bus.PublicarNotificacao(new DomainNotification("local", "Local não encontrado pelo Id!")).Wait();
                return Task.FromResult(false);
            }

            local.DefinirNomeLocal(message.NomeLocal);

            if (!string.IsNullOrEmpty(message.IdentificadorExterno))
                local.DefinirIdentificadorExterno(message.IdentificadorExterno);

            if (!string.IsNullOrEmpty(message.Descricao))
                local.DefinirDescricao(message.Descricao);

            if (message.ReservaLocal)
                local.ReservarLocal();
            else
                local.RemoverReservaLocal();

            local.DefinirLotacaoMaxima(message.LotacaoMaxima);

            _localRepository.Atualizar(local);
            if (Commit())
            {
                Bus.PublicarEvento(new LocalAtualizadoEvent(local.Id, local.IdentificadorExterno, local.NomeLocal, local.Descricao, local.ReservaLocal, local.LotacaoMaxima));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoverLocalCommand message, CancellationToken cancellationToken)
        {
            Local local = _localRepository.ObterPorId(message.Id);
            _localRepository.Remover(local);

            Bus.PublicarEvento(new LocalRemovidoEvent(local.Id)).Wait();
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _localRepository.Dispose();
        }
    }
}
