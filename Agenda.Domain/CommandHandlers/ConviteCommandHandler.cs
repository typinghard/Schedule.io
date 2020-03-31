using Agenda.Domain.Commands;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;
using Agenda.Domain.Enums;
using Agenda.Domain.Events;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Domain.CommandHandlers
{
    public class ConviteCommandHandler : CommandHandler,
        IRequestHandler<RegistrarConviteCommand, bool>,
        IRequestHandler<AtualizarConviteCommand, bool>,
        IRequestHandler<RemoverConviteCommand, bool>
    {
        private readonly IConviteRepository _conviteRepository;
        private readonly IMediatorHandler Bus;

        public ConviteCommandHandler(IConviteRepository conviteRepository,
                                     IUnitOfWork uow,
                                     IMediatorHandler bus,
                                     INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            this._conviteRepository = conviteRepository;
            this.Bus = bus;
        }

        public Task<bool> Handle(RegistrarConviteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            Convite convite = new Convite(message.Id, message.EventoId, message.UsuarioId, message.EmailConvidado);

            convite.AtualizarStatusConvite(message.Status);

            if (message.Permissoes.ConvidaUsuario)
                convite.Permissoes.PodeConvidar();
            else
                convite.Permissoes.NaoPodeConvidar();

            if (message.Permissoes.VeListaDeConvidados)
                convite.Permissoes.PodeVerListaDeConvidados();
            else
                convite.Permissoes.NaoPodeVerListaDeConvidados();

            if (message.Permissoes.ModificaEvento)
                convite.Permissoes.PodeModificarEvento();
            else
                convite.Permissoes.NaoPodeModificarEvento();

            _conviteRepository.Adicionar(convite);

            if (Commit())
            {
                Bus.PublicarEvento(new ConviteRegistradoEvent(convite.Id, convite.EventoId, convite.UsuarioId, convite.EmailConvidado, convite.Status, convite.Permissoes));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(AtualizarConviteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            Convite convite = _conviteRepository.ObterPorId(message.Id);
            if (convite == null)
            {
                Bus.PublicarNotificacao(new DomainNotification("convite", "Convite não encontrado pelo Id!")).Wait();
                return Task.FromResult(false);
            }

            if (!string.IsNullOrEmpty(message.UsuarioId))
                convite.DefinirUsuarioId(message.UsuarioId);

            convite.DefinirEmailConvidado(message.EmailConvidado);
            convite.DefinirEventoId(message.EventoId);
            convite.AtualizarStatusConvite(message.Status);

            if (message.Permissoes.ConvidaUsuario)
                convite.Permissoes.PodeConvidar();
            else
                convite.Permissoes.NaoPodeConvidar();

            if (message.Permissoes.VeListaDeConvidados)
                convite.Permissoes.PodeVerListaDeConvidados();
            else
                convite.Permissoes.NaoPodeVerListaDeConvidados();

            if (message.Permissoes.ModificaEvento)
                convite.Permissoes.PodeModificarEvento();
            else
                convite.Permissoes.NaoPodeModificarEvento();


            _conviteRepository.Atualizar(convite);
            if (Commit())
            {
                Bus.PublicarEvento(new ConviteAtualizadoEvent(convite.Id, convite.EventoId, convite.UsuarioId, convite.EmailConvidado, convite.Status, convite.Permissoes));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoverConviteCommand message, CancellationToken cancellationToken)
        {
            Convite convite = _conviteRepository.ObterPorId(message.Id);
            _conviteRepository.Remover(convite);

            if (Commit())
                Bus.PublicarEvento(new EventoAgendaRemovidoEvent(convite.Id)).Wait();
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _conviteRepository.Dispose();
        }


    }
}
