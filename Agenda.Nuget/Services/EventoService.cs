using Agenda.Domain.Commands;
using Agenda.Domain.Core.Communication.Mediator;
using Agenda.Domain.Core.Messages.CommonMessages.Notifications;
using Agenda.Domain.Interfaces;
using MediatR;
using ScheduleIo.Nuget.Interfaces;
using ScheduleIo.Nuget.Models;
using Agenda.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Models;

namespace ScheduleIo.Nuget.Services
{
    internal class EventoService : ServiceBase, IEventoService
    {
        private readonly IEventoAgendaRepository _eventoAgendaRepository;
        private readonly IMediatorHandler _bus;

        private readonly IAgendaUsuarioRepository _agendaUsuarioRepository;

        public EventoService(IEventoAgendaRepository eventoAgendaRepository,
                             IAgendaUsuarioRepository agendaUsuarioRepository,
                             IMediatorHandler bus,
                             INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _bus = bus;
            _eventoAgendaRepository = eventoAgendaRepository;
            _agendaUsuarioRepository = agendaUsuarioRepository;
        }

        public Guid Gravar(Evento evento)
        {
            Guid eventoId;
            if (evento.Id == Guid.Empty)
            {
                eventoId = Guid.NewGuid();
                var listConvites = MontaConviteDomainModel(eventoId, evento);

                _bus.EnviarComando(new RegistrarEventoAgendaCommand(eventoId, evento.AgendaId, evento.IdentificadorExterno, evento.Titulo,
                    evento.Descricao, listConvites, evento.Local.Value, evento.DataInicio, evento.DataFinal,
                    evento.DataLimiteConfirmacao.Value, evento.QuantidadeMinimaDeUsuarios, evento.OcupaUsuario,
                    evento.Publico,
                    new Agenda.Domain.Models.TipoEvento(Guid.Empty, evento.Tipo.Nome, evento.Tipo.Descricao),
                    evento.Frequencia)).Wait();
            }
            else
            {
                eventoId = evento.Id;

                var listConvites = MontaConviteDomainModel(evento.Id, evento);

                ValidarComando();

                _bus.EnviarComando(new RegistrarEventoAgendaCommand(evento.Id, evento.AgendaId, evento.IdentificadorExterno, evento.Titulo,
                        evento.Descricao, listConvites, evento.Local.Value, evento.DataInicio, evento.DataFinal,
                        evento.DataLimiteConfirmacao.Value, evento.QuantidadeMinimaDeUsuarios, evento.OcupaUsuario,
                        evento.Publico,
                        new Agenda.Domain.Models.TipoEvento(Guid.Empty, evento.Tipo.Nome, evento.Tipo.Descricao),
                        evento.Frequencia)).Wait();
            }

            ValidarComando();
            return eventoId;
        }

        public bool Excluir(Guid eventoId)
        {
            ValidarComando();
            _bus.EnviarComando(new RemoverEventoAgendaCommand(eventoId)).Wait();
            return true;
        }

        public Evento Obter(Guid eventoId)
        {
            var eventoModel = _eventoAgendaRepository.ObterPorId(eventoId);

            if (eventoModel == null)
            {
                throw new ScheduleIoException(new List<string>() { "Evento não encontrado!" });
            }

            ValidarComando();

            var listConvitesVm = new List<Models.Convite>();

            foreach (var conviteModel in eventoModel.Convites)
            {
                var conviteVm = new Models.Convite()
                {
                    Id = conviteModel.Id,
                    EventoId = conviteModel.EventoId,
                    UsuarioId = conviteModel.UsuarioId,
                    Permissoes = new Models.PermissoesConvite()
                    {
                        ModificaEvento = conviteModel.Permissoes.ModificaEvento,
                        ConvidaUsuario = conviteModel.Permissoes.ConvidaUsuario,
                        VeListaDeConvidados = conviteModel.Permissoes.VeListaDeConvidados
                    }
                };

                listConvitesVm.Add(conviteVm);
            }

            var eventoVm = new Evento()
            {
                Id = eventoModel.Id,
                CriadoAs = eventoModel.CriadoAs,
                AtualizadoAs = eventoModel.AtualizadoAs,
                AgendaId = eventoModel.AgendaId,
                IdentificadorExterno = eventoModel.IdentificadorExterno,
                Titulo = eventoModel.Titulo,
                Descricao = eventoModel.Descricao,
                Convites = listConvitesVm,
                Local = eventoModel.Local,
                DataInicio = eventoModel.DataInicio,
                DataFinal = eventoModel.DataFinal,
                DataLimiteConfirmacao = eventoModel.DataLimiteConfirmacao,
                QuantidadeMinimaDeUsuarios = eventoModel.QuantidadeMinimaDeUsuarios,
                OcupaUsuario = eventoModel.OcupaUsuario,
                Publico = eventoModel.Publico,
                Frequencia = eventoModel.Frequencia,
                Tipo = new Models.TipoEvento()
                {
                    Id = eventoModel.Tipo.Id,
                    CriadoAs = eventoModel.Tipo.CriadoAs,
                    AtualizadoAs = eventoModel.Tipo.AtualizadoAs,
                    Nome = eventoModel.Tipo.Nome,
                    Descricao = eventoModel.Tipo.Descricao
                }
            };

            return eventoVm;
        }


        private List<Agenda.Domain.Models.Convite> MontaConviteDomainModel(Guid eventoId, Evento evento)
        {
            var usuarioAgenda = _agendaUsuarioRepository.ObterPorId(evento.AgendaId);

            var listConvites = new List<Agenda.Domain.Models.Convite>();
            foreach (var conviteVM in evento.Convites)
            {
                var convite = new Agenda.Domain.Models.Convite(Guid.Empty, eventoId, conviteVM.UsuarioId);

                if (usuarioAgenda.UsuarioId == conviteVM.UsuarioId)
                    convite.AtualizarStatusConvite(EnumStatusConviteEvento.Sim);
                else
                    convite.AtualizarStatusConvite(EnumStatusConviteEvento.Aguardando_Confirmacao);

                if (conviteVM.Permissoes.ConvidaUsuario)
                    convite.Permissoes.PodeConvidar();
                else
                    convite.Permissoes.NaoPodeConvidar();

                if (conviteVM.Permissoes.ModificaEvento)
                    convite.Permissoes.PodeModificarEvento();
                else
                    convite.Permissoes.NaoPodeModificarEvento();

                if (conviteVM.Permissoes.VeListaDeConvidados)
                    convite.Permissoes.PodeVerListaDeConvidados();
                else
                    convite.Permissoes.NaoPodeModificarEvento();


                listConvites.Add(convite);
            }

            return listConvites;
        }
    }
}
