using System;
using System.Threading;
using System.Threading.Tasks;
using Agenda.Domain.Commands;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;
using Agenda.Domain.Events;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using MediatR;


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

            EventoAgenda eventoAgenda = new EventoAgenda(message.AgendaId, message.IdentificadorExterno, message.Titulo, message.Descricao, message.Pessoas, message.Local, message.DataInicio, message.DataFinal, message.DataLimiteConfirmacao, message.QuantidadeMinimaDeUsuarios, message.OcupaUsuario, message.EventoPublico, message.TipoEvento, message.EnumFrequencia);
            _eventoAgendaRepository.Adicionar(eventoAgenda);

            if (Commit())
            {
                Bus.PublicarEvento(new EventoAgendaRegistradoEvent(eventoAgenda.Id, eventoAgenda.AgendaId, eventoAgenda.IdentificadorExterno, eventoAgenda.Titulo, eventoAgenda.Descricao, eventoAgenda.Pessoas, eventoAgenda.Local, eventoAgenda.DataInicio, eventoAgenda.DataFinal, eventoAgenda.DataLimiteConfirmacao, eventoAgenda.QuantidadeMinimaDeUsuarios, eventoAgenda.OcupaUsuario, eventoAgenda.EventoPublico, eventoAgenda.TipoEvento, eventoAgenda.EnumFrequencia));
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

            eventoAgenda.DefinirAgenda(message.AgendaId);
            eventoAgenda.DefinirIdentificadorExterno(message.IdentificadorExterno);
            eventoAgenda.DefinirTitulo(message.Titulo);
            eventoAgenda.DefinirDescricao(message.Descricao);

            foreach (Guid pessoa in message.Pessoas)
                eventoAgenda.AdicionarPessoa(pessoa);

            eventoAgenda.DefinirLocal(message.Local);
            eventoAgenda.DefinirDatas(message.DataInicio, message.DataFinal);
            ///essa data limite confirmação acho que tá zuado pq ela posso passar nulo também
            eventoAgenda.DefinirDataLimiteConfirmacao(message.DataLimiteConfirmacao);
            eventoAgenda.DefinirQuantidadeMinimaDeUsuarios(message.QuantidadeMinimaDeUsuarios);

            ///verificar se é assim ou se pode ser da forma igual ao metodo de DefirnirEventoPublicoOuPrivado
            if (message.OcupaUsuario)
                eventoAgenda.OcuparUsuario();
            else
                eventoAgenda.DesocuparUsuario();

            eventoAgenda.DefinirEventoPublicoOuPrivado(message.EventoPublico);
            eventoAgenda.DefinirTipoEvento(message.TipoEvento);
            eventoAgenda.DefinirFrequencia(message.EnumFrequencia);

            _eventoAgendaRepository.Atualizar(eventoAgenda);
            if (Commit())
            {
                Bus.PublicarEvento(new EventoAgendaAtualizadoEvent(eventoAgenda.Id, eventoAgenda.AgendaId, eventoAgenda.IdentificadorExterno, eventoAgenda.Titulo, eventoAgenda.Descricao, eventoAgenda.Pessoas, eventoAgenda.Local, eventoAgenda.DataInicio, eventoAgenda.DataFinal, eventoAgenda.DataLimiteConfirmacao, eventoAgenda.QuantidadeMinimaDeUsuarios, eventoAgenda.OcupaUsuario, eventoAgenda.EventoPublico, eventoAgenda.TipoEvento, eventoAgenda.EnumFrequencia));
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
