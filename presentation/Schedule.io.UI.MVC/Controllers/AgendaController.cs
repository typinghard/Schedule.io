//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Schedule.io.Core.Core.DomainObjects;
//using Schedule.io.Interfaces;
//using ScheduleIo.Nuget.Models;

//namespace Agenda.UI.Web.Controllers
//{
//    public class AgendaController : Controller
//    {
//        //private static DateTime _DataInicio;

//        IAgendaService _agendaService;
//        IUsuarioService _usuarioService;
//        ILocalService _localService;
//        IEventoService _eventoService;

//        public AgendaController(IAgendaService agendaService,
//                                IUsuarioService usuarioService,
//                                ILocalService localService,
//                                IEventoService eventoService)
//        {
//            _agendaService = agendaService;
//            _usuarioService = usuarioService;
//            _localService = localService;
//            _eventoService = eventoService;
//        }


//        // GET: Agenda
//        public IActionResult Index()
//        {
//            try
//            {
//                //var evento = _eventoService.Obter("b4e5b559-f79a-437a-afea-9397416cd262");
//                //evento.OcupaUsuario = true;
//                //_eventoService.Gravar(evento);
//                //GravarAgenda();
//                //GravarUsuario();
//                //GravarLocal();
//                //GravarEvento(false);
//                //GravarEvento(true);
//                //GravarEvento(true, true);
//                //ExcluirEvento();

//                //var agendaId = "35fb8c94-a7c7-488f-80db-13312e0b7cf8";
//                //var dataInicio = DateTime.Now.AddDays(-7);
//                //var dataFinal = DateTime.Now;

//                //var listEventos = _eventoService.ObterEventosPorPeriodo(agendaId, dataInicio, dataFinal);

//                return Content("Funcionou!");
//            }
//            catch (ScheduleIoException ex)
//            {
//                var retorno = string.Join(", ", ex.ScheduleIoMessages.Select(x => x).ToList());
//                Console.WriteLine(retorno);

//                return Content(retorno);
//            }
//        }

//        private void GravarAgenda(bool novo = false)
//        {
//            var agenda = AgendaVM(novo);
//            _agendaService.Gravar(agenda);
//        }

//        private void GravarUsuario(bool novo = false)
//        {
//            var usuario = UsuarioVM(novo);
//            //usuario.Email = "um-novo-usuario@email.com";
//            _usuarioService.Gravar(usuario);
//        }

//        private void GravarLocal(bool novo = false)
//        {
//            var local = LocalVM(novo);
//            _localService.Gravar(local);
//        }

//        private void GravarEvento(bool novoEvento = false, bool usarAgendaExistente = false)
//        {
//            var evento = new Evento();
//            if (novoEvento)
//            {
//                var agenda = new ScheduleIo.Nuget.Models.Agenda();
//                if (usarAgendaExistente)
//                {
//                    agenda = AgendaVM(false, "35fb8c94-a7c7-488f-80db-13312e0b7cf8");
//                }
//                else
//                {
//                    agenda = AgendaVM(true);
//                    _agendaService.Gravar(agenda);
//                }

//                evento = EventoVM(true, agenda, agenda.Usuario);
//            }
//            else
//                evento = EventoVM(false);

//            _eventoService.Gravar(evento);
//        }

//        private void ExcluirEvento()
//        {
//            // var evento = EventoVM(false, null, null, "9bcec73c-1206-4ebd-a761-62e962aaa42c");
//            _eventoService.Excluir("9bcec73c-1206-4ebd-a761-62e962aaa42c");
//        }

//        private ScheduleIo.Nuget.Models.Agenda AgendaVM(bool novaAgenda, string agendaId = null)
//        {
//            if (novaAgenda)
//            {
//                return new ScheduleIo.Nuget.Models.Agenda()
//                {
//                    Titulo = "Agenda da Limpeza",
//                    Descricao = "",
//                    Publico = false,
//                    Usuario = UsuarioVM(true)
//                };
//            }
//            else
//            {
//                var agenda1 = new ScheduleIo.Nuget.Models.Agenda();
//                if (!string.IsNullOrEmpty(agendaId))
//                {
//                    agenda1 = _agendaService.Obter(agendaId);
//                    if (agenda1.Usuario == null)
//                        agenda1.Usuario = _usuarioService.Obter("adfada26-37c9-44f0-8d64-fb774d9b9c83");
//                }
//                else
//                {
//                    var listAgendas = _agendaService.ObterTodas();
//                    agenda1 = listAgendas.FirstOrDefault();

//                }

//                if (agenda1 != null)
//                {
//                    if (!agenda1.Titulo.Contains("Atualizada"))
//                        agenda1.Titulo = agenda1.Titulo + " - Atualizada";
//                    else
//                        agenda1.Titulo = agenda1.Titulo.Split('-')[0].Trim();
//                    agenda1.Publico = !agenda1.Publico;

//                    return agenda1;
//                }
//            }

//            return null;
//        }

//        private ScheduleIo.Nuget.Models.Usuario UsuarioVM(bool donoAgenda, bool usuarioExistente = false, string id = null)
//        {
//            if (usuarioExistente)
//            {
//                if (string.IsNullOrEmpty(id))
//                    id = "d2d7882f-8e4c-45f0-b9b8-bf043470b792";

//                var usuario = _usuarioService.Obter(id);

//                if (usuario != null)
//                {
//                    if (!usuario.Email.Contains("-"))
//                        usuario.Email = usuario.Email + "-atualizado";
//                    else
//                        usuario.Email = usuario.Email.Split('-')[0].Trim();

//                    return usuario;
//                }

//                return null;
//            }

//            if (donoAgenda)
//                return new Usuario()
//                {
//                    Email = "usuario_donoagenda@mail.com"
//                };
//            else
//            {
//                return new Usuario()
//                {
//                    Email = "usuario_convidado@gmail.com"
//                };
//            }
//        }

//        private Local LocalVM(bool novoLocal, string id = null)
//        {
//            if (novoLocal)
//            {
//                return new Local()
//                {
//                    LotacaoMaxima = 3,
//                    Nome = "Casa",
//                };
//            }
//            else
//            {
//                if (string.IsNullOrEmpty(id))
//                    id = "79c6945a-dc71-499f-b247-fd4567f2dd55";

//                var local = _localService.Obter(id);

//                if (local != null)
//                {
//                    if (!local.Nome.Contains("Atualizado"))
//                        local.Nome = local.Nome + " - Atualizado";
//                    else
//                        local.Nome = local.Nome.Split('-')[0].Trim();

//                    local.Reserva = !local.Reserva;
//                    local.LotacaoMaxima++;

//                    return local;
//                }
//            }

//            return null;
//        }

//        private Evento EventoVM(bool novoEvento, ScheduleIo.Nuget.Models.Agenda agenda = null, Usuario usuario = null, string eventoId = null)
//        {
//            if (novoEvento)
//            {
//                return new Evento()
//                {
//                    AgendaId = agenda.Id,
//                    UsuarioId = usuario.Id,
//                    IdentificadorExterno = string.Empty,
//                    Titulo = "Limpar a casa",
//                    Descricao = string.Empty,
//                    Convites = new List<Convite>()
//                    {
//                        new Convite()
//                        {
//                            Usuario = UsuarioVM(false),
//                            Permissoes = new PermissoesConvite()
//                            {
//                                ConvidaUsuario = true,
//                                ModificaEvento = true,
//                                VeListaDeConvidados = true,
//                            }
//                        }
//                    },
//                    Local = LocalVM(true),
//                    //DataInicio = _DataInicio != DateTime.MinValue ? _DataInicio : DateTime.Now,
//                    DataInicio = DateTime.Now,
//                    DataFinal = DateTime.MinValue,
//                    DataLimiteConfirmacao = DateTime.Now,
//                    QuantidadeMinimaDeUsuarios = 2,
//                    OcupaUsuario = false,
//                    Publico = false,
//                    Frequencia = Nao_Repete,
//                    Tipo = new TipoEvento()
//                    {
//                        Nome = "Limpeza",
//                        Descricao = string.Empty
//                    }
//                };
//            }
//            else
//            {
//                if (string.IsNullOrEmpty(eventoId))
//                    eventoId = "26d0dedf-b30c-48d9-aa5e-31e428d71dae";

//                var evento = _eventoService.Obter(eventoId);

//                evento.Local = LocalVM(false, evento.Local.Id);

//                var listConvites = evento.Convites;
//                foreach (var conviteVM in listConvites)
//                {
//                    if (!string.IsNullOrEmpty(conviteVM.Usuario.Id) && evento.UsuarioId == conviteVM.Usuario.Id)
//                    {
//                        conviteVM.Permissoes = new PermissoesConvite()
//                        {
//                            Id = conviteVM.Id,
//                            ConvidaUsuario = true,
//                            ModificaEvento = true,
//                            VeListaDeConvidados = true
//                        };
//                    }
//                    else
//                    {
//                        conviteVM.Permissoes = new PermissoesConvite()
//                        {
//                            Id = conviteVM.Id,
//                            ConvidaUsuario = false,
//                            ModificaEvento = false,
//                            VeListaDeConvidados = true
//                        };
//                    }
//                }
//                evento.Convites = listConvites;
//                evento.DataInicio = evento.DataInicio.AddDays(1);
//                evento.DataLimiteConfirmacao = evento.DataLimiteConfirmacao.Value.AddDays(1);
//                evento.DataFinal = evento.DataInicio;

//                if (!evento.Titulo.Contains("Atualizado"))
//                    evento.Titulo = evento.Titulo + " - Atualizado";
//                else
//                    evento.Titulo = evento.Titulo.Split('-')[0].Trim();


//                return evento;
//            }
//        }

//        // GET: Agenda/Details/5
//        public IActionResult Details(int id)
//        {
//            return View();
//        }

//        // GET: Agenda/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Agenda/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(IFormCollection collection)
//        {
//            try
//            {
//                // TODO: Add insert logic here

//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: Agenda/Edit/5
//        public IActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: Agenda/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(int id, IFormCollection collection)
//        {
//            try
//            {
//                // TODO: Add update logic here

//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: Agenda/Delete/5
//        public IActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: Agenda/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Delete(int id, IFormCollection collection)
//        {
//            try
//            {
//                // TODO: Add delete logic here

//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}