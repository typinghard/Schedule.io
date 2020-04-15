using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Interfaces;
using Schedule.io.Interfaces.Services;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;

namespace Schedule.io.UI.Web.Controllers
{
    public class AgendaController : Controller
    {
        //private static DateTime _DataInicio;

        IAgendaService _agendaService;
        IUsuarioService _usuarioService;
        ILocalService _localService;
        IEventoService _eventoService;
        ITipoEventoService _tipoEventoService;

        public AgendaController(IAgendaService agendaService,
                                IUsuarioService usuarioService,
                                ILocalService localService,
                                IEventoService eventoService,
                                ITipoEventoService tipoEventoService)
        {
            _agendaService = agendaService;
            _usuarioService = usuarioService;
            _localService = localService;
            _eventoService = eventoService;
            _tipoEventoService = tipoEventoService;
        }


        // GET: Agenda
        public IActionResult Index()
        {
            try
            {
                //var usuario = _usuarioService.Obter("a1b2757d-98ad-41ab-96be-9c25159bf5e2");
                //var usuarioConvidado = _usuarioService.Obter("16332ddf-f82a-4d3d-b787-2a09dff50c2e");
                //var agenda = _agendaService.Obter("5802c228-aef8-426b-8641-c7507d68ef56");

                //var usuario = new Usuario("nuss_donoagendaSqlServer@email.com");
                //var usuarioConvidado = new Usuario("usuario_convidado@email.com");

                ////_usuarioService.Gravar(usuario);
                //_usuarioService.Gravar(usuarioConvidado);

                //var agenda = new Agenda(usuario.Id, "AGENDA ARQUITETURA NOVA");
                //var agendaUsuario = new io.Models.ValueObjects.AgendaUsuario(agenda.Id, usuario.Id);
                //agenda.AdicionarAgendaDoUsuario(agendaUsuario);

                //_agendaService.Gravar(agenda);

                //_agendaService.Excluir("f52d78c0-65bd-460a-8c07-3b42d4234320");

                //var evento1 = new Evento(agenda.Id, usuario.Id, "EVENTO SQL SERVER", DateTime.Now.AddDays(3));
                //evento1.DefinirDataFinal(DateTime.Now.AddDays(3));
                //evento1.OcuparUsuario();
                ////evento1.AdicionarConvite(new io.Models.ValueObjects.Convite(evento1.Id, usuario.Id));
                //evento1.AdicionarConvite(new Convite(evento1.Id, usuarioConvidado.Id));

                //_eventoService.Gravar(evento1);

                //var evento2 = new Evento(agenda.Id, usuario.Id, "EVENTO SQL SERVER", DateTime.Now.AddDays(3));
                //evento2.DefinirDataFinal(DateTime.Now.AddDays(3));
                //evento2.OcuparUsuario();
                //evento2.AdicionarConvite(new io.Models.ValueObjects.Convite(evento1.Id, usuario.Id));
                //evento2.AdicionarConvite(new Convite(evento2.Id, usuarioConvidado.Id));

                //_eventoService.Gravar(evento2);
                //--##--

                //var usuario = _usuarioService.Obter("4a27288f-431e-4176-88c1-0f690b7d8caf");
                //var usuarioConvidado = _usuarioService.Obter("f885a91b-5549-42be-a5c4-209571b49b05");
                //var agenda = _agendaService.Obter("c1758117-bdf2-4100-91ff-952985cd8f29");


                //"0bfc6537-372a-4879-b045-1864376f5251"
                //"7bb70eb8-d4db-49c4-8441-e9cd74e8c726"
                //var evento = _eventoService.Obter("0bfc6537-372a-4879-b045-1864376f5251");
                //evento.DefinirTitulo(evento.Titulo + " - ATUALIZADO!!!");
                //foreach (var convite in evento.Convites)
                //{
                //    if (convite.UsuarioId == evento.UsuarioIdCriador)
                //    {
                //        convite.AtualizarStatusConvite(Enums.EnumStatusConviteEvento.Sim);
                //        convite.Permissoes.PodeConvidar();
                //        convite.Permissoes.PodeVerListaDeConvidados();
                //        convite.Permissoes.PodeModificarEvento();
                //    }
                //}
                //_eventoService.Gravar(evento);

                //var usuario = _usuarioService.Obter("adfada26-37c9-44f0-8d64-fb774d9b9c83");
                //var agenda = _agendaService.Obter("35fb8c94-a7c7-488f-80db-13312e0b7cf8");

                //var eventos1 = _eventoService.Listar(agenda.Id);
                //var eventos2 = _eventoService.Listar(agenda.Id, usuario.Id);

                //var dataInicio = DateTime.Now.AddDays(-7);
                //var dataFinal = DateTime.Now;
                //var eventos3 = _eventoService.Listar(agenda.Id, dataInicio, dataFinal);

                //var local = _localService.Listar();

                //var novaAgenda = new Agenda(usuario.Id, "AGENDA ARQUITETURA NOVA");
                //var agendaUsuario = new io.Models.ValueObjects.AgendaUsuario(novaAgenda.Id, usuario.Id);
                //novaAgenda.AdicionarAgendaDoUsuario(agendaUsuario);

                //_agendaService.Gravar(novaAgenda);

                //var novaAgenda2 = new Agenda(usuario.Id, "AGENDA ARQUITETURA NOVA2");
                //agendaUsuario = new io.Models.ValueObjects.AgendaUsuario(novaAgenda2.Id, usuario.Id);
                //novaAgenda2.AdicionarAgendaDoUsuario(agendaUsuario);
                //_agendaService.Gravar(novaAgenda2);

                //_agendaService.Excluir(agenda.Id);

                //var agendaExitente = _agendaService.Obter(novaAgenda.Id);

                //agendaExitente.DefinirTitulo(agendaExitente.Titulo + " - ATUALIZADA!");
                //agendaExitente.AdicionarAgendaDoUsuario(agendaUsuario);
                //_agendaService.Gravar(agendaExitente);

                //var lista = _agendaService.Listar(usuario.Id).ToList();

                //var tipoEvento = _tipoEventoService.Obter("b2854a98-b8b1-4c1b-8088-83a36e0482d0");

                //var tipoEvento = new TipoEvento("TIPO TESTE", "descricao teste");
                //_tipoEventoService.Gravar(tipoEvento);

                //var evento = new Evento(agenda.Id, usuario.Id, "EVENTO SQL SERVER", DateTime.Now.AddDays(2), tipoEvento.Id);
                //evento.DefinirDatas(DateTime.Now.AddDays(2), DateTime.Now.AddDays(2));

                //_eventoService.Gravar(evento);


                //var evento1 = new Evento(agenda.Id, usuario.Id, "EVENTO SQL SERVER", DateTime.Now.AddDays(3));
                //evento1.DefinirDataFinal(DateTime.Now.AddDays(3));

                //evento1.AdicionarConvite(new io.Models.ValueObjects.Convite(evento1.Id, usuario.Id));
                //evento1.AdicionarConvite(new Convite(evento1.Id, usuarioConvidado.Id));

                //_eventoService.Gravar(evento1);

                //var evento2 = _eventoService.Obter("427bbe40-39b7-48f7-a049-85606ef0f49b");

                //_eventoService.Excluir(evento2.Id);

                return Content("Funcionou!");
            }
            catch (ScheduleIoException ex)
            {
                var retorno = string.Join(", ", ex.ScheduleIoMessages.Select(x => x).ToList());
                Console.WriteLine(retorno);

                return Content(retorno);
            }
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