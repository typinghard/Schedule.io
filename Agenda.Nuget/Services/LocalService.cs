using Agenda.Domain.Commands;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;
using Agenda.Domain.Interfaces;
using MediatR;
using ScheduleIo.Nuget.Interfaces;
using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Services
{
    internal class LocalService : ServiceBase, ILocalService
    {
        private readonly ILocalRepository _localRepository;
        private readonly IMediatorHandler _bus;

        public LocalService(ILocalRepository localRepository,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _localRepository = localRepository;
            _bus = bus;
        }

        public string Gravar(Local local)
        {
            var localId = string.Empty;
            if (string.IsNullOrEmpty(local.Id))
            {
                localId = Guid.NewGuid().ToString();
                _bus.EnviarComando(new RegistrarLocalCommand(localId, local.IdentificadorExterno, local.Nome, local.Descricao,
                                                             local.Reserva, local.LotacaoMaxima)).Wait();
            }
            else
            {
                localId = local.Id;
                _bus.EnviarComando(new AtualizarLocalCommand(local.Id, local.IdentificadorExterno, local.Nome, local.Descricao,
                                                         local.Reserva, local.LotacaoMaxima)).Wait();
            }

            ValidarComando();
            return localId;
        }


        public bool Excluir(string localId)
        {
            _bus.EnviarComando(new RemoverLocalCommand(localId)).Wait();
            ValidarComando();
            return true;
        }

        public Local Obter(string localId)
        {
            var local = _localRepository.ObterPorId(localId);

            if (local == null)
            {
                throw new ScheduleIoException(new List<string>() { "Local não encontrado!" });
            }
            ValidarComando();

            return new Local()
            {
                Id = local.Id,
                CriadoAs = local.CriadoAs,
                AtualizadoAs = local.AtualizadoAs,
                IdentificadorExterno = local.IdentificadorExterno,
                Nome = local.Nome,
                Descricao = local.Descricao,
                LotacaoMaxima = local.LotacaoMaxima,
                Reserva = local.Reserva
            };

        }
    }
}
