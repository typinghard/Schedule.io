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
using Agenda.Domain.Core.DomainObjects;
using System.Linq;
using System.Collections.Generic;

namespace Agenda.Domain.CommandHandlers
{
    public class EventoAgendaCommandHandler : CommandHandler,
        IRequestHandler<RegistrarEventoAgendaCommand, bool>,
        IRequestHandler<AtualizarEventoAgendaCommand, bool>,
        IRequestHandler<RemoverEventoAgendaCommand, bool>
    {
        private readonly IEventoAgendaRepository _eventoAgendaRepository;
        private readonly ILocalRepository _localRepository;
        private readonly IMediatorHandler Bus;

        private readonly IUsuarioRepository _usuarioRepository;

        public EventoAgendaCommandHandler(ILocalRepository localRepository,
                                          IEventoAgendaRepository eventoAgendaRepository,
                                          IUsuarioRepository usuarioRepository,
                                          IUnitOfWork uow,
                                          IMediatorHandler bus,
                                          INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            this._localRepository = localRepository;
            this._eventoAgendaRepository = eventoAgendaRepository;
            this.Bus = bus;

            this._usuarioRepository = usuarioRepository;
        }

        public Task<bool> Handle(RegistrarEventoAgendaCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            EventoAgenda eventoAgenda = new EventoAgenda(message.Id, message.AgendaId, message.UsuarioId, message.Titulo, message.DataInicio, message.Tipo);

            if (!string.IsNullOrEmpty(message.IdentificadorExterno))
                eventoAgenda.DefinirIdentificadorExterno(message.IdentificadorExterno);

            if (!string.IsNullOrEmpty(message.Descricao))
                eventoAgenda.DefinirDescricao(message.Descricao);

            if (message.OcupaUsuario)
                eventoAgenda.OcuparUsuario();

            if (message.Publico)
                eventoAgenda.TornarEventoPublico();

            if (message.Frequencia != EnumFrequencia.Nao_Repete)
                eventoAgenda.DefinirFrequencia(message.Frequencia);

            if (message.DataFinal != DateTime.MinValue)
                eventoAgenda.DefinirDatas(eventoAgenda.DataInicio, message.DataFinal);

            if (message.DataLimiteConfirmacao.HasValue && message.DataLimiteConfirmacao != DateTime.MinValue)
                eventoAgenda.DefinirDataLimiteConfirmacao(message.DataLimiteConfirmacao.Value);

            if (!message.LocalId.EhVazio())
                eventoAgenda.DefinirLocal(message.LocalId);

            if (message.QuantidadeMinimaDeUsuarios > 0)
                eventoAgenda.DefinirQuantidadeMinimaDeUsuarios(message.QuantidadeMinimaDeUsuarios);

            eventoAgenda.Tipo.DefinirNome(message.Tipo.Nome);

            if (!string.IsNullOrEmpty(message.Tipo.Descricao))
                eventoAgenda.Tipo.DefinirDescricao(message.Tipo.Descricao);

            Validacoes(eventoAgenda);

            _eventoAgendaRepository.Adicionar(eventoAgenda);

            if (Commit())
            {
                Bus.PublicarEvento(new EventoAgendaRegistradoEvent(eventoAgenda.Id, eventoAgenda.AgendaId, eventoAgenda.UsuarioId,
                                    eventoAgenda.IdentificadorExterno, eventoAgenda.Titulo, eventoAgenda.Descricao,
                                    (List<Convite>)eventoAgenda.Convites, eventoAgenda.LocalId, eventoAgenda.DataInicio, eventoAgenda.DataFinal,
                                    eventoAgenda.DataLimiteConfirmacao, eventoAgenda.QuantidadeMinimaDeUsuarios,
                                    eventoAgenda.OcupaUsuario, eventoAgenda.Publico, eventoAgenda.Tipo, eventoAgenda.Frequencia));
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

            if (!string.IsNullOrEmpty(message.LocalId))
                eventoAgenda.DefinirLocal(message.LocalId);

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

            Validacoes(eventoAgenda);

            _eventoAgendaRepository.Atualizar(eventoAgenda);
            if (Commit())
            {
                Bus.PublicarEvento(new EventoAgendaAtualizadoEvent(eventoAgenda.Id, eventoAgenda.AgendaId, eventoAgenda.UsuarioId,
                                   eventoAgenda.IdentificadorExterno, eventoAgenda.Titulo, eventoAgenda.Descricao,
                                   (List<Convite>)eventoAgenda.Convites, eventoAgenda.LocalId, eventoAgenda.DataInicio, eventoAgenda.DataFinal,
                                   eventoAgenda.DataLimiteConfirmacao, eventoAgenda.QuantidadeMinimaDeUsuarios,
                                   eventoAgenda.OcupaUsuario, eventoAgenda.Publico, eventoAgenda.Tipo, eventoAgenda.Frequencia));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoverEventoAgendaCommand message, CancellationToken cancellationToken)
        {
            EventoAgenda eventoAgenda = _eventoAgendaRepository.ObterPorId(message.Id);
            _eventoAgendaRepository.Remover(eventoAgenda);

            if (Commit())
                Bus.PublicarEvento(new EventoAgendaRemovidoEvent(eventoAgenda.Id)).Wait();

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _eventoAgendaRepository.Dispose();
        }


        private void Validacoes(EventoAgenda eventoAgenda)
        {
            ValidaQuantidadeDeUsuariosNoLocal(eventoAgenda);
            ValidaEventosUsuarioOcupadoNoMesmoHorario(eventoAgenda);
        }

        private void ValidaQuantidadeDeUsuariosNoLocal(EventoAgenda eventoAgenda)
        {
            if (!string.IsNullOrEmpty(eventoAgenda.LocalId))
            {
                var local = _localRepository.ObterPorId(eventoAgenda.LocalId);
                if (local != null && eventoAgenda.QuantidadeMinimaDeUsuarios > local.LotacaoMaxima)
                    throw new DomainException("A quantidade máxima de usuários não pode ser maior que a lotação máxima do local!");
            }

        }

        private void ValidaEventosUsuarioOcupadoNoMesmoHorario(EventoAgenda eventoAgenda)
        {
            if (eventoAgenda.OcupaUsuario)
            {
                //eventos_do_usuario
                var eventosUsuario = _eventoAgendaRepository.ObterTodosEventosDoUsuario(eventoAgenda.AgendaId, eventoAgenda.UsuarioId);

                //para cada evento validar se não há eventos no mesmo horário como ocupado
                var cont = eventosUsuario.Count(x => x.DataInicio == eventoAgenda.DataInicio && x.OcupaUsuario == true);
                if (cont > 0)
                    throw new DomainException("Usuário não pode dois ou mais eventos no mesmo horário marcados como ocupado!");
            }
        }
    }
}
