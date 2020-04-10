using Schedule.io.Core.Interfaces;
using Schedule.io.Interfaces;
using Schedule.io.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Schedule.io.Core.Models;
using Schedule.io.Core.Core.Communication.Mediator;
using MediatR;
using Schedule.io.Core.Core.Messages.CommonMessages.Notifications;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Enums;
using Schedule.io.Core.Commands.EventoAgendaCommands;
using Schedule.io.Core.Commands.ConviteCommands;

namespace Schedule.io.Services
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

        #region Obter

        public IEnumerable<Evento> ObterEventosPorPeriodo(string agendaId, DateTime dataInicial, DateTime dataFinal)
        {
            var listEventoAgenda = _eventoAgendaRepository.ObterEventosPorPeriodo(agendaId, dataInicial, dataFinal);

            return MontaEventoVM(listEventoAgenda);
        }


        public IEnumerable<Evento> ObterTodos(string agendaId)
        {
            var listEventoAgenda = _eventoAgendaRepository.ObterEventosDaAgenda(agendaId);

            return MontaEventoVM(listEventoAgenda);
        }

        public Evento Obter(string eventoId)
        {
            var eventoModel = _eventoAgendaRepository.ObterPorId(eventoId);

            return MontaEventoVM(eventoModel);
        }

        private Evento MontaEventoVM(EventoAgenda eventoModel)
        {
            if (eventoModel == null)
                throw new ScheduleIoException(new List<string>() { "Evento não encontrado!" });

            var convites = _conviteRepository.ObterConvitesPorEventoId(eventoModel.Id);

            dynamic local = null;
            if (eventoModel.LocalId != null)
                local = _localService.Obter(eventoModel.LocalId);

            ValidarComando();

            return new Evento()
            {
                Id = eventoModel.Id,
                CriadoAs = eventoModel.CriadoAs,
                AtualizadoAs = eventoModel.AtualizadoAs,
                AgendaId = eventoModel.AgendaId,
                UsuarioId = eventoModel.UsuarioId,
                IdentificadorExterno = eventoModel.IdentificadorExterno,
                Titulo = eventoModel.Titulo,
                Descricao = eventoModel.Descricao,
                Convites = MontaConvitesVM(convites),
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
        }

        private IList<Evento> MontaEventoVM(IList<EventoAgenda> listEventoModel)
        {
            if (listEventoModel == null)
                throw new ScheduleIoException(new List<string>() { "Não há eventos nesta agenda!" });

            var listEventoVM = new List<Evento>();
            foreach (var eventoModel in listEventoModel)
            {
                var convites = _conviteRepository.ObterConvitesPorEventoId(eventoModel.Id);
                var local = _localService.Obter(eventoModel.LocalId);

                ValidarComando();

                listEventoVM.Add(new Evento()
                {
                    Id = eventoModel.Id,
                    CriadoAs = eventoModel.CriadoAs,
                    AtualizadoAs = eventoModel.AtualizadoAs,
                    AgendaId = eventoModel.AgendaId,
                    UsuarioId = eventoModel.UsuarioId,
                    IdentificadorExterno = eventoModel.IdentificadorExterno,
                    Titulo = eventoModel.Titulo,
                    Descricao = eventoModel.Descricao,
                    Convites = MontaConvitesVM(convites),
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
                });
            }

            return listEventoVM;
        }

        private Models.Usuario MontaUsuarioVM(Core.Models.Convite conviteModel)
        {
            dynamic usuarioVM = null;
            if (!string.IsNullOrEmpty(conviteModel.UsuarioId) && conviteModel.UsuarioId != Guid.Empty.ToString())
                usuarioVM = _usuarioService.Obter(conviteModel.UsuarioId);

            if (usuarioVM == null)
                usuarioVM = new Models.Usuario()
                {
                    Email = conviteModel.EmailConvidado
                };

            return usuarioVM;
        }

        private List<Models.Convite> MontaConvitesVM(IList<Core.Models.Convite> convitesModel)
        {
            if (convitesModel == null || convitesModel.Count == 0)
                return new List<Models.Convite>();

            var listConvitesVm = new List<Models.Convite>();

            if (convitesModel.Any(x => x.Permissoes == null))
                return new List<Models.Convite>();

            foreach (var conviteModel in convitesModel)
            {
                var conviteVm = new Models.Convite()
                {
                    Id = conviteModel.Id,
                    EventoId = conviteModel.EventoId,
                    Usuario = MontaUsuarioVM(conviteModel),
                    Permissoes = new Models.PermissoesConvite()
                    {
                        ModificaEvento = conviteModel.Permissoes.ModificaEvento,
                        ConvidaUsuario = conviteModel.Permissoes.ConvidaUsuario,
                        VeListaDeConvidados = conviteModel.Permissoes.VeListaDeConvidados
                    }
                };

                listConvitesVm.Add(conviteVm);
            }

            return listConvitesVm;
        }
        #endregion

        #region Gravar
        public string Gravar(Evento evento)
        {
            if (string.IsNullOrEmpty(evento.AgendaId))
                throw new ScheduleIoException(new List<string>() { "Agenda não informado!" });

            if (string.IsNullOrEmpty(evento.UsuarioId))
                throw new ScheduleIoException(new List<string>() { "Usuario não informado!" });

            if (evento.Local != null)
                evento.Local.Id = _localService.Gravar(evento.Local);

            if (string.IsNullOrEmpty(evento.Id))
                evento.Id = RegistrarEvento(evento);
            else
                evento.Id = AtualizarEvento(evento);

            if (evento.Convites != null && evento.Convites.Count > 0)
                GravarConvite(evento);

            ValidarComando();
            return evento.Id;
        }

        private string RegistrarEvento(Evento evento)
        {
            evento.Id = Guid.NewGuid().ToString();
            var listConvites = MontaConviteDomainModel(evento);

            _bus.EnviarComando(new RegistrarEventoAgendaCommand(evento.Id, evento.AgendaId, evento.UsuarioId, evento.IdentificadorExterno, evento.Titulo,
                evento.Descricao, listConvites, evento.Local != null ? evento.Local.Id : string.Empty, evento.DataInicio, evento.DataFinal,
                evento.DataLimiteConfirmacao, evento.QuantidadeMinimaDeUsuarios, evento.OcupaUsuario,
                evento.Publico,
                new Core.Models.TipoEvento(string.Empty, evento.Tipo.Nome, evento.Tipo.Descricao),
                evento.Frequencia)).Wait();

            return evento.Id;
        }

        private string AtualizarEvento(Evento evento)
        {
            var listConvites = MontaConviteDomainModel(evento);

            _bus.EnviarComando(new AtualizarEventoAgendaCommand(evento.Id, evento.AgendaId, evento.UsuarioId, evento.IdentificadorExterno, evento.Titulo,
                    evento.Descricao, listConvites, evento.Local != null ? evento.Local.Id : string.Empty, evento.DataInicio, evento.DataFinal,
                    evento.DataLimiteConfirmacao, evento.QuantidadeMinimaDeUsuarios, evento.OcupaUsuario,
                    evento.Publico,
                    new Core.Models.TipoEvento(string.Empty, evento.Tipo.Nome, evento.Tipo.Descricao),
                    evento.Frequencia)).Wait();

            return evento.Id;
        }

        private void GravarConvite(Evento evento)
        {
            var listConvites = MontaConviteDomainModel(evento);
            foreach (var convite in listConvites)
            {
                if (string.IsNullOrEmpty(convite.Id) || Guid.Parse(convite.Id) == Guid.Empty)
                    _bus.EnviarComando(new RegistrarConviteCommand(Guid.NewGuid().ToString(), convite.EventoId, convite.UsuarioId,
                                                                   convite.EmailConvidado, convite.Status, convite.Permissoes));
                else
                    _bus.EnviarComando(new AtualizarConviteCommand(convite.Id, convite.EventoId, convite.UsuarioId, convite.EmailConvidado,
                                                                   convite.Status, convite.Permissoes));
            }

            ValidarComando();
        }
        #endregion Gravar

        #region Excluir
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

        private void RemoverLocal(Models.Local local)
        {
            if (local != null)
                _localService.Excluir(local.Id);
        }

        private void RemoverConvites(Evento evento)
        {
            var listConvitesDomain = MontaConviteDomainModel(evento);
            foreach (var convite in listConvitesDomain)
                _bus.EnviarComando(new RemoverConviteCommand(convite.Id)).Wait();
        }
        #endregion Excluir

        private List<Core.Models.Convite> MontaConviteDomainModel(Evento evento)
        {
            if (evento.Convites == null || evento.Convites.Count == 0)
                return new List<Core.Models.Convite>();

            var usuarioAgenda = _agendaUsuarioRepository.ObterAgendaDoUsuario(evento.AgendaId, evento.UsuarioId);

            if (usuarioAgenda == null)
                throw new ScheduleIoException(new List<string>() { "Agenda do usuário não encontrada!" });

            var listConvites = new List<Core.Models.Convite>();

            var conviteDono = MontaConviteDonoEvento(evento);
            if (conviteDono != null)
                evento.Convites.Add(conviteDono);

            foreach (var conviteVM in evento.Convites)
            {

                if (string.IsNullOrEmpty(conviteVM.Id))
                    conviteVM.Id = Guid.Empty.ToString();

                var convite = new Core.Models.Convite(conviteVM.Id, evento.Id, conviteVM.Usuario.Id, conviteVM.Usuario.Email);

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

        private Models.Convite MontaConviteDonoEvento(Evento evento)
        {
            if (evento.Convites.Where(x => x.Usuario.Id == evento.UsuarioId).Count() == 0)
            {
                var usuario = _usuarioService.Obter(evento.UsuarioId);
                return new Models.Convite()
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
