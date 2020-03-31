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
using System.Linq;

namespace ScheduleIo.Nuget.Services
{
    internal class EventoService : ServiceBase, IEventoService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILocalService _localService;
        private readonly IEventoAgendaRepository _eventoAgendaRepository;
        private readonly IAgendaUsuarioRepository _agendaUsuarioRepository;
        private readonly IConviteRepository _conviteRepository;
        private readonly IMediatorHandler _bus;

        public EventoService(ILocalService localService,
                             IUsuarioService usuarioService,
                             IEventoAgendaRepository eventoAgendaRepository,
                             IAgendaUsuarioRepository agendaUsuarioRepository,
                             IConviteRepository conviteRepository,
                             IMediatorHandler bus,
                             INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _bus = bus;
            _localService = localService;
            _usuarioService = usuarioService;
            _eventoAgendaRepository = eventoAgendaRepository;
            _agendaUsuarioRepository = agendaUsuarioRepository;
            _conviteRepository = conviteRepository;
        }
        public string Gravar(Evento evento)
        {
            if (string.IsNullOrEmpty(evento.AgendaId))
            {
                throw new ScheduleIoException(new List<string>() { "Agenda não informado!" });
            }

            if (string.IsNullOrEmpty(evento.UsuarioId))
            {
                throw new ScheduleIoException(new List<string>() { "Usuario não informado!" });
            }

            if (evento.Local != null)
                evento.Local.Id = _localService.Gravar(evento.Local);

            var listConvites = new List<Agenda.Domain.Models.Convite>();
            if (string.IsNullOrEmpty(evento.Id))
            {
                evento.Id = Guid.NewGuid().ToString();
                listConvites = MontaConviteDomainModel(evento);

                _bus.EnviarComando(new RegistrarEventoAgendaCommand(evento.Id, evento.AgendaId, evento.UsuarioId, evento.IdentificadorExterno, evento.Titulo,
                    evento.Descricao, listConvites, evento.Local.Id, evento.DataInicio, evento.DataFinal,
                    evento.DataLimiteConfirmacao.Value, evento.QuantidadeMinimaDeUsuarios, evento.OcupaUsuario,
                    evento.Publico,
                    new Agenda.Domain.Models.TipoEvento(string.Empty, evento.Tipo.Nome, evento.Tipo.Descricao),
                    evento.Frequencia)).Wait();
            }
            else
            {
                listConvites = MontaConviteDomainModel(evento);

                _bus.EnviarComando(new AtualizarEventoAgendaCommand(evento.Id, evento.AgendaId, evento.UsuarioId, evento.IdentificadorExterno, evento.Titulo,
                        evento.Descricao, listConvites, evento.Local.Id, evento.DataInicio, evento.DataFinal,
                        evento.DataLimiteConfirmacao.Value, evento.QuantidadeMinimaDeUsuarios, evento.OcupaUsuario,
                        evento.Publico,
                        new Agenda.Domain.Models.TipoEvento(string.Empty, evento.Tipo.Nome, evento.Tipo.Descricao),
                        evento.Frequencia)).Wait();
            }

            if (evento.Convites.Count > 0)
            {
                GravaConvite(listConvites);
            }

            ValidarComando();
            return evento.Id;
        }

        public bool Excluir(string eventoId)
        {
            var evento = Obter(eventoId);

            if (evento == null)
                throw new ScheduleIoException(new List<string>() { "Evento não encontrado!" });

            RemoverLocal(evento.Local);
            RemoverConvites(evento);

            ValidarComando();
            _bus.EnviarComando(new RemoverEventoAgendaCommand(eventoId)).Wait();
            return true;
        }

        private void RemoverConvites(Evento evento)
        {
            var listConvitesDomain = MontaConviteDomainModel(evento);
            foreach (var convite in listConvitesDomain)
                _bus.EnviarComando(new RemoverConviteCommand(convite.Id)).Wait();
        }

        private void RemoverLocal(Models.Local local)
        {
            if (local != null)
                _localService.Excluir(local.Id);
        }

        public Evento Obter(string eventoId)
        {
            var eventoModel = _eventoAgendaRepository.ObterPorId(eventoId);

            if (eventoModel == null)
                throw new ScheduleIoException(new List<string>() { "Evento não encontrado!" });

            ValidarComando();

            var listConvitesVm = new List<Models.Convite>();

            var convites = _conviteRepository.ObterConvitesPorEventoId(eventoModel.Id);
            if (convites != null)
                foreach (var conviteModel in convites)
                {
                    var usuarioVM = _usuarioService.Obter(conviteModel.UsuarioId);

                    if (usuarioVM == null)
                        usuarioVM = new Models.Usuario()
                        {
                            Email = conviteModel.EmailConvidado
                        };

                    var conviteVm = new Models.Convite()
                    {
                        Id = conviteModel.Id,
                        EventoId = conviteModel.EventoId,
                        Usuario = usuarioVM,
                        Permissoes = new Models.PermissoesConvite()
                        {
                            ModificaEvento = conviteModel.Permissoes.ModificaEvento,
                            ConvidaUsuario = conviteModel.Permissoes.ConvidaUsuario,
                            VeListaDeConvidados = conviteModel.Permissoes.VeListaDeConvidados
                        }
                    };

                    listConvitesVm.Add(conviteVm);
                }

            var local = _localService.Obter(eventoModel.Local);

            var eventoVm = new Evento()
            {
                Id = eventoModel.Id,
                CriadoAs = eventoModel.CriadoAs,
                AtualizadoAs = eventoModel.AtualizadoAs,
                AgendaId = eventoModel.AgendaId,
                UsuarioId = eventoModel.UsuarioId,
                IdentificadorExterno = eventoModel.IdentificadorExterno,
                Titulo = eventoModel.Titulo,
                Descricao = eventoModel.Descricao,
                Convites = listConvitesVm,
                Local = local,
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

        private void GravaConvite(List<Agenda.Domain.Models.Convite> convites)
        {
            foreach (var convite in convites)
            {
                if (string.IsNullOrEmpty(convite.Id) || Guid.Parse(convite.Id) == Guid.Empty)
                    _bus.EnviarComando(new RegistrarConviteCommand(Guid.NewGuid().ToString(), convite.EventoId, convite.UsuarioId, convite.EmailConvidado, convite.Status, convite.Permissoes));
                else
                    _bus.EnviarComando(new AtualizarConviteCommand(convite.Id, convite.EventoId, convite.UsuarioId, convite.EmailConvidado, convite.Status, convite.Permissoes));
            }

            ValidarComando();
        }

        private void GravaUsuarioConvite(Evento evento)
        {
            var listConvitesAtualizados = new List<Models.Convite>();
            foreach (var convite in evento.Convites)
            {
                var usuario = _usuarioService.Obter(convite.Usuario.Id);
                if (usuario == null)
                {
                    convite.Usuario.Id = _usuarioService.Gravar(convite.Usuario);
                    listConvitesAtualizados.Add(convite);
                }
            }

            evento.Convites = listConvitesAtualizados;
        }


        private List<Agenda.Domain.Models.Convite> MontaConviteDomainModel(Evento evento)
        {
            var usuarioAgenda = _agendaUsuarioRepository.ObterPorAgendaIdEUsuarioId(evento.AgendaId, evento.UsuarioId);

            if (usuarioAgenda == null)
                throw new ScheduleIoException(new List<string>() { "Agenda do usuário não encontrada!" });

            var listConvites = new List<Agenda.Domain.Models.Convite>();

            var conviteDono = MontaConviteDonoEvento(evento);
            if (conviteDono != null)
                evento.Convites.Add(conviteDono);

            foreach (var conviteVM in evento.Convites)
            {

                if (string.IsNullOrEmpty(conviteVM.Id))
                    conviteVM.Id = Guid.Empty.ToString();

                var convite = new Agenda.Domain.Models.Convite(conviteVM.Id, evento.Id, conviteVM.Usuario.Id, conviteVM.Usuario.Email);

                if (usuarioAgenda.UsuarioId == conviteVM.Usuario.Id)
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

        private ScheduleIo.Nuget.Models.Convite MontaConviteDonoEvento(Evento evento)
        {
            if (evento.Convites.Where(x => x.Usuario.Id == evento.UsuarioId).Count() == 0)
            {
                var usuario = _usuarioService.Obter(evento.UsuarioId);
                return new ScheduleIo.Nuget.Models.Convite()
                {
                    EventoId = evento.Id,
                    Usuario = usuario,
                    Status = EnumStatusConviteEvento.Sim,
                    Permissoes = new Models.PermissoesConvite()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ConvidaUsuario = true,
                        ModificaEvento = true,
                        VeListaDeConvidados = true
                    }
                };
            }

            return null;
        }
    }
}
