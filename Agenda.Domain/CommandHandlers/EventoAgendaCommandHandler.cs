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

        private readonly IAgendaUsuarioRepository _agendaUsuarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public EventoAgendaCommandHandler(ILocalRepository localRepository,
                                          IEventoAgendaRepository eventoAgendaRepository,
                                          IUsuarioRepository usuarioRepository,
                                          IAgendaUsuarioRepository agendaUsuarioRepository,
                                          IUnitOfWork uow,
                                          IMediatorHandler bus,
                                          INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            this._localRepository = localRepository;
            this._eventoAgendaRepository = eventoAgendaRepository;
            this.Bus = bus;

            this._usuarioRepository = usuarioRepository;
            this._agendaUsuarioRepository = agendaUsuarioRepository;
        }

        public Task<bool> Handle(RegistrarEventoAgendaCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            EventoAgenda eventoAgenda = new EventoAgenda(message.Id, message.AgendaId, message.Titulo, message.DataInicio, message.Tipo);

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

            if (message.DataLimiteConfirmacao != DateTime.MinValue)
                eventoAgenda.DefinirDataLimiteConfirmacao(message.DataLimiteConfirmacao.Value);

            if (message.Convites.Count > 0)
            {
                var listaConvites = ValidaStatusConviteUsuarios(eventoAgenda);
                foreach (Convite convite in listaConvites)
                    eventoAgenda.AdicionarConvite(convite);
            }

            if (!message.Local.EhVazio())
                eventoAgenda.DefinirLocal(message.Local);

            if (message.QuantidadeMinimaDeUsuarios > 0)
                eventoAgenda.DefinirQuantidadeMinimaDeUsuarios(message.QuantidadeMinimaDeUsuarios);

            eventoAgenda.Tipo.DefinirNome(message.Tipo.Nome);
            eventoAgenda.Tipo.DefinirDescricao(message.Tipo.Descricao);

            Validacoes(eventoAgenda);

            _eventoAgendaRepository.Adicionar(eventoAgenda);

            if (Commit())
            {
                Bus.PublicarEvento(new EventoAgendaRegistradoEvent(eventoAgenda.Id, eventoAgenda.AgendaId,
                                    eventoAgenda.IdentificadorExterno, eventoAgenda.Titulo, eventoAgenda.Descricao,
                                    (List<Convite>)eventoAgenda.Convites, eventoAgenda.Local, eventoAgenda.DataInicio, eventoAgenda.DataFinal,
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

            //foreach (Guid pessoaId in message.Usuarios)
            //    eventoAgenda.AdicionarPessoa(pessoaId);

            if (message.Convites.Count > 0)
            {
                var listaConvites = ValidaStatusConviteUsuarios(eventoAgenda);
                foreach (Convite convite in listaConvites)
                    eventoAgenda.AdicionarConvite(convite);
            }

            eventoAgenda.DefinirLocal(message.Local);

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
                Bus.PublicarEvento(new EventoAgendaAtualizadoEvent(eventoAgenda.Id, eventoAgenda.AgendaId,
                                   eventoAgenda.IdentificadorExterno, eventoAgenda.Titulo, eventoAgenda.Descricao,
                                   (List<Convite>)eventoAgenda.Convites, eventoAgenda.Local, eventoAgenda.DataInicio, eventoAgenda.DataFinal,
                                   eventoAgenda.DataLimiteConfirmacao, eventoAgenda.QuantidadeMinimaDeUsuarios,
                                   eventoAgenda.OcupaUsuario, eventoAgenda.Publico, eventoAgenda.Tipo, eventoAgenda.Frequencia));
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


        private void Validacoes(EventoAgenda eventoAgenda)
        {
            ValidaQuantidadeDeUsuariosNoLocal(eventoAgenda);
            ValidaStatusConviteUsuarios(eventoAgenda);
        }

        private void ValidaQuantidadeDeUsuariosNoLocal(EventoAgenda eventoAgenda)
        {
            if (eventoAgenda.Local.HasValue)
            {
                var local = _localRepository.ObterPorId(eventoAgenda.Local.Value);
                if (eventoAgenda.QuantidadeMinimaDeUsuarios > local.LotacaoMaxima)
                {
                    throw new DomainException("A quantidade máxima de usuários não pode ser maior que a lotação máxima do local!");
                }
            }

        }

        private List<Convite> ValidaStatusConviteUsuarios(EventoAgenda eventoAgenda)
        {
            /* Descrição:
             * - Ao criar um evento com vários usuários, os OUTROS usuários ficaram pendentes de confirmação, porém o usuário que criou já é confirmado
             */

            //usuario criador do evento - marcado como confirmado
            var usuarioAgenda = _agendaUsuarioRepository.ObterPorId(eventoAgenda.AgendaId);

            var listConvites = new List<Convite>();
            foreach (var novoConvite in eventoAgenda.Convites)
            {
                var convite = new Convite(Guid.Empty, eventoAgenda.Id, novoConvite.UsuarioId);

                if (usuarioAgenda.UsuarioId == novoConvite.UsuarioId)
                    convite.AtualizarStatusConvite(EnumStatusConviteEvento.Sim);
                else
                    convite.AtualizarStatusConvite(EnumStatusConviteEvento.Aguardando_Confirmacao);

                if (novoConvite.Permissoes.ConvidaUsuario)
                    convite.Permissoes.PodeConvidar();
                else
                    convite.Permissoes.NaoPodeConvidar();

                if (novoConvite.Permissoes.ModificaEvento)
                    convite.Permissoes.PodeModificarEvento();
                else
                    convite.Permissoes.NaoPodeModificarEvento();

                if (novoConvite.Permissoes.VeListaDeConvidados)
                    convite.Permissoes.PodeVerListaDeConvidados();
                else
                    convite.Permissoes.NaoPodeModificarEvento();


                listConvites.Add(convite);
            }

            //os demais marcado como pendencia
            return listConvites;
        }

        private void ValidaUsuarioOcupadoNoMesmoHorario(Guid agendaId, Convite convite)
        {
            /* Descrição:
            *  - Um usuário não pode ter dois eventos marcados como OCUPADO coincidindo horário
            */

            //usuario
            var usuario = _usuarioRepository.ObterPorId(convite.UsuarioId);

            //eventos_do_usuario
            var eventosUsuario = _eventoAgendaRepository.ObterTodosEventosUsuario(agendaId, convite.UsuarioId);

            //para cada evento validar se não há eventos no mesmo horário como ocupado
            var cont = eventosUsuario.Select(x => x.DataInicio == x.DataInicio && x.OcupaUsuario).Count();
            if (cont > 0)
            {
                throw new DomainException("Usuário não pode dois ou mais eventos no mesmo horário marcados como ocupado!");
            }
        }
    }
}
