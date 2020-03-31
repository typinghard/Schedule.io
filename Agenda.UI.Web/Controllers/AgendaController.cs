using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleIo.Nuget.Interfaces;
using ScheduleIo.Nuget.Models;

namespace Agenda.UI.Web.Controllers
{
    public class AgendaController : Controller
    {
        IAgendaService _agendaService;
        IUsuarioService _usuarioService;
        ILocalService _localService;
        IEventoService _eventoService;

        public AgendaController(IAgendaService agendaService,
                                IUsuarioService usuarioService,
                                ILocalService localService,
                                IEventoService eventoService)
        {
            _agendaService = agendaService;
            _usuarioService = usuarioService;
            _localService = localService;
            _eventoService = eventoService;
        }


        // GET: Agenda
        public IActionResult Index()
        {
            try
            {

                GravarAgenda();
                GravarUsuario();
                GravarLocal();
                GravarEvento();

                return Content("Funcionou!");
            }
            catch (Agenda.Domain.Core.DomainObjects.ScheduleIoException ex)
            {
                var retorno = string.Join(", ", ex.ScheduleIoMessages.Select(x => x).ToList());
                Console.WriteLine(retorno);

                return Content(retorno);
            }
        }

        private void GravarAgenda()
        {
            var agenda = AgendaVM(false);
            _agendaService.Gravar(agenda);
        }

        private void GravarUsuario()
        {
            var usuario = UsuarioVM(false, true);
            usuario.Email = "um-novo-usuario@email.com";
            _usuarioService.Gravar(usuario);
        }

        private void GravarLocal()
        {
            var local = LocalVM(false, null);
            _localService.Gravar(local);
        }

        private void GravarEvento()
        {
            var evento = EventoVM(false);
            _eventoService.Gravar(evento);
        }

        private ScheduleIo.Nuget.Models.Agenda AgendaVM(bool novaAgenda)
        {
            if (novaAgenda)
            {
                return new ScheduleIo.Nuget.Models.Agenda()
                {
                    Titulo = "Agenda da Limpeza",
                    Descricao = "",
                    Publico = false,
                    Usuario = UsuarioVM(true)
                };
            }
            else
            {
                var listAgendas = _agendaService.ObterTodas();

                var agenda1 = listAgendas.FirstOrDefault();

                if (agenda1 != null)
                {
                    if (!agenda1.Titulo.Contains("Atualizada"))
                        agenda1.Titulo = agenda1.Titulo + " - Atualizada";
                    else
                        agenda1.Titulo = agenda1.Titulo.Split('-')[0].Trim();
                    agenda1.Publico = !agenda1.Publico;

                    return agenda1;
                }
            }

            return null;
        }

        private ScheduleIo.Nuget.Models.Usuario UsuarioVM(bool donoAgenda, bool usuarioExistente = false, string id = null)
        {
            if (usuarioExistente)
            {
                if (string.IsNullOrEmpty(id))
                    id = "4e0af403-5029-4db1-849d-9ee189382614";

                var usuario = _usuarioService.Obter(id);

                if (usuario != null)
                {
                    if (!usuario.Email.Contains("-"))
                        usuario.Email = usuario.Email + "-atualizado";
                    else
                        usuario.Email = usuario.Email.Split('-')[0].Trim();

                    return usuario;
                }

                return null;
            }

            if (donoAgenda)
                return new Usuario()
                {
                    Email = "usuario_donoagenda@mail.com"
                };
            else
            {
                return new Usuario()
                {
                    Email = "usuario_convidado@gmail.com"
                };
            }
        }

        private Local LocalVM(bool novoLocal, string id = null)
        {
            if (novoLocal)
            {
                return new Local()
                {
                    LotacaoMaxima = 3,
                    Nome = "Casa",
                };
            }
            else
            {
                if (string.IsNullOrEmpty(id))
                    id = "a539bec7-acbb-4bca-b424-de0db430d9eb";

                var local = _localService.Obter(id);

                if (local != null)
                {
                    if (!local.Nome.Contains("Atualizado"))
                        local.Nome = local.Nome + " - Atualizado";
                    else
                        local.Nome = local.Nome.Split('-')[0].Trim();

                    local.Reserva = !local.Reserva;
                    local.LotacaoMaxima++;

                    return local;
                }
            }

            return null;
        }

        private Evento EventoVM(bool novoEvento, ScheduleIo.Nuget.Models.Agenda agenda = null, Usuario usuario = null, string eventoId = null)
        {
            if (novoEvento)
            {
                return new Evento()
                {
                    AgendaId = agenda.Id,
                    UsuarioId = usuario.Id,
                    IdentificadorExterno = string.Empty,
                    Titulo = "Limpar a casa",
                    Descricao = string.Empty,
                    Convites = new List<Convite>()
                    {
                        new Convite()
                        {
                            Usuario = UsuarioVM(false),
                            Permissoes = new PermissoesConvite()
                            {
                                ConvidaUsuario = true,
                                ModificaEvento = true,
                                VeListaDeConvidados = true,
                            }
                        }
                    },
                    Local = LocalVM(true),
                    DataInicio = DateTime.Now,
                    DataFinal = DateTime.MinValue,
                    DataLimiteConfirmacao = DateTime.Now,
                    QuantidadeMinimaDeUsuarios = 2,
                    OcupaUsuario = false,
                    Publico = false,
                    Frequencia = Domain.Enums.EnumFrequencia.Nao_Repete,
                    Tipo = new TipoEvento()
                    {
                        Nome = "Limpeza",
                        Descricao = string.Empty
                    }
                };
            }
            else
            {
                if (string.IsNullOrEmpty(eventoId))
                    eventoId = "e9bde481-9a90-4746-bb8f-02ce2d988ffd";

                var evento = _eventoService.Obter(eventoId);

                evento.Local = LocalVM(false, evento.Local.Id);

                var listConvites = evento.Convites;
                foreach (var conviteVM in listConvites)
                {
                    if (evento.UsuarioId == conviteVM.Usuario.Id)
                    {
                        conviteVM.Permissoes = new PermissoesConvite()
                        {
                            Id = conviteVM.Id,
                            ConvidaUsuario = true,
                            ModificaEvento = true,
                            VeListaDeConvidados = true
                        };
                    }
                    else
                    {
                        conviteVM.Permissoes = new PermissoesConvite()
                        {
                            Id = conviteVM.Id,
                            ConvidaUsuario = false,
                            ModificaEvento = false,
                            VeListaDeConvidados = true
                        };
                    }
                }
                evento.Convites = listConvites;
                evento.DataInicio = evento.DataInicio.AddDays(1);
                evento.DataLimiteConfirmacao = evento.DataLimiteConfirmacao.Value.AddDays(1);
                evento.DataFinal = evento.DataInicio;

                if (!evento.Titulo.Contains("Atualizado"))
                    evento.Titulo = evento.Titulo + " - Atualizado";
                else
                    evento.Titulo = evento.Titulo.Split('-')[0].Trim();


                return evento;
            }

            return null;
        }

        // GET: Agenda/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Agenda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agenda/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Agenda/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Agenda/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Agenda/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Agenda/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}