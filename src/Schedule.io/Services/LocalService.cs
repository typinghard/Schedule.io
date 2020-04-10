using MediatR;
using Schedule.io.Core.Commands.LocalCommands;
using Schedule.io.Core.Core.Communication.Mediator;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Core.Interfaces;
using Schedule.io.Interfaces;
using Schedule.io.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Services
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
            if (string.IsNullOrEmpty(local.Id))
            {
                local.Id = Guid.NewGuid().ToString();
                _bus.EnviarComando(new RegistrarLocalCommand(local.Id, local.IdentificadorExterno, local.Nome, local.Descricao,
                                                             local.Reserva, local.LotacaoMaxima)).Wait();
            }
            else
                _bus.EnviarComando(new AtualizarLocalCommand(local.Id, local.IdentificadorExterno, local.Nome, local.Descricao,
                                                         local.Reserva, local.LotacaoMaxima)).Wait();

            ValidarComando();
            return local.Id;
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
                throw new ScheduleIoException(new List<string>() { "Local não encontrado!" });

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
