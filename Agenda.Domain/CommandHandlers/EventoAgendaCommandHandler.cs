using System;
using System.Threading;
using System.Threading.Tasks;
using Agenda.Domain.Commands;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;
using Agenda.Domain.Events;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Agenda.Domain.Core.Helpers;
using MediatR;
using Agenda.Domain.Enums;

namespace Agenda.Domain.CommandHandlers
{
    public class EventoAgendaCommandHandler : CommandHandler,
        IRequestHandler<RegistrarEventoAgendaCommand, bool>,
        IRequestHandler<AtualizarEventoAgendaCommand, bool>,
        IRequestHandler<RemoverEventoAgendaCommand, bool>
    {
        private readonly IEventoAgendaRepository _eventoAgendaRepository;
        private readonly IMediatorHandler Bus;

        public EventoAgendaCommandHandler(IEventoAgendaRepository eventoAgendaRepository,
                                          IUnitOfWork uow,
                                          IMediatorHandler bus,
                                          INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            this._eventoAgendaRepository = eventoAgendaRepository;
            this.Bus = bus;
        }

        public Task<bool> Handle(RegistrarEventoAgendaCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            EventoAgenda eventoAgenda = new EventoAgenda(message.AgendaId, message.Titulo, message.DataInicio, message.Tipo);

            if (!string.IsNullOrEmpty(message.IdentificadorExterno))
                eventoAgenda.DefinirIdentificadorExterno(message.IdentificadorExterno);

            if (!string.IsNullOrEmpty(message.Descricao))
                eventoAgenda.DefinirDescricao(message.Descricao);

            if (message.Pessoas.Count > 0)
                foreach (string pessoaId in message.Pessoas)
                    eventoAgenda.AdicionarPessoa(pessoaId);

            if (!message.Local.EhVazio())
                eventoAgenda.DefinirLocal(message.Local);

            if (message.DataFinal != DateTime.MinValue)
                eventoAgenda.DefinirDatas(eventoAgenda.DataInicio, message.DataFinal);

            if (message.DataLimiteConfirmacao != DateTime.MinValue)
                eventoAgenda.DefinirDataLimiteConfirmacao(message.DataLimiteConfirmacao.Value);

            if (message.QuantidadeMinimaDeUsuarios > 0)
                eventoAgenda.DefinirQuantidadeMinimaDeUsuarios(message.QuantidadeMinimaDeUsuarios);

            if (message.OcupaUsuario)
                eventoAgenda.OcuparUsuario();

            if (message.Publico)
                eventoAgenda.TornarEventoPublico();

            if (message.Frequencia != EnumFrequencia.Nao_Repete)
                eventoAgenda.DefinirFrequencia(message.Frequencia);

            eventoAgenda.Tipo.DefinirNome(message.Tipo.Nome);
            eventoAgenda.Tipo.DefinirDescricao(message.Tipo.Descricao);

            _eventoAgendaRepository.Adicionar(eventoAgenda);

            if (Commit())
            {
                Bus.PublicarEvento(new EventoAgendaRegistradoEvent(eventoAgenda.Id, eventoAgenda.AgendaId, eventoAgenda.IdentificadorExterno, eventoAgenda.Titulo, eventoAgenda.Descricao, eventoAgenda.Pessoas, eventoAgenda.Local, eventoAgenda.DataInicio, eventoAgenda.DataFinal, eventoAgenda.DataLimiteConfirmacao, eventoAgenda.QuantidadeMinimaDeUsuarios, eventoAgenda.OcupaUsuario, eventoAgenda.Publico, eventoAgenda.Tipo, eventoAgenda.Frequencia));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(AtualizarEventoAgendaCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            EventoAgenda eventoAgenda = _eventoAgendaRepository.ObterPorId(message.Id);
            if (eventoAgenda == null)
            {
                Bus.PublicarNotificacao(new DomainNotification("eventoAgenda", "EventoAgenda não encontrado pelo Id!")).Wait();
                return Task.FromResult(false);
            }

            eventoAgenda.DefinirIdentificadorExterno(message.IdentificadorExterno);
            eventoAgenda.DefinirDescricao(message.Descricao);

            foreach (string pessoa in message.Pessoas)
                eventoAgenda.AdicionarPessoa(pessoa);

            if (message.DataFinal == DateTime.MinValue)
                eventoAgenda.DefinirDataInicial(message.DataInicio);
            else
                eventoAgenda.DefinirDatas(eventoAgenda.DataInicio, message.DataFinal);

            eventoAgenda.DefinirDataLimiteConfirmacao(message.DataLimiteConfirmacao.GetValueOrDefault());
            eventoAgenda.DefinirQuantidadeMinimaDeUsuarios(message.QuantidadeMinimaDeUsuarios);

            if (message.OcupaUsuario)
                eventoAgenda.OcuparUsuario();
            else
                eventoAgenda.DesocuparUsuario();

            if (message.Publico)
                eventoAgenda.TornarEventoPublico();
            else
                eventoAgenda.TornarEventoPrivado();

            eventoAgenda.Tipo.DefinirNome(message.Tipo.Nome);
            eventoAgenda.Tipo.DefinirDescricao(message.Tipo.Descricao);
            eventoAgenda.DefinirFrequencia(message.Frequencia);

            _eventoAgendaRepository.Atualizar(eventoAgenda);
            if (Commit())
            {
                Bus.PublicarEvento(new EventoAgendaAtualizadoEvent(eventoAgenda.Id, eventoAgenda.AgendaId, eventoAgenda.IdentificadorExterno, eventoAgenda.Titulo, eventoAgenda.Descricao, eventoAgenda.Pessoas, eventoAgenda.Local, eventoAgenda.DataInicio, eventoAgenda.DataFinal, eventoAgenda.DataLimiteConfirmacao, eventoAgenda.QuantidadeMinimaDeUsuarios, eventoAgenda.OcupaUsuario, eventoAgenda.Publico, eventoAgenda.Tipo, eventoAgenda.Frequencia));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoverEventoAgendaCommand message, CancellationToken cancellationToken)
        {
            EventoAgenda eventoAgenda = _eventoAgendaRepository.ObterPorId(message.Id);
            _eventoAgendaRepository.Remover(eventoAgenda);

            Bus.PublicarEvento(new EventoAgendaRemovidoEvent(eventoAgenda.Id)).Wait();
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _eventoAgendaRepository.Dispose();
        }
    }
}
