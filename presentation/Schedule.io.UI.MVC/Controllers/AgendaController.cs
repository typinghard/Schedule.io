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

namespace Schedule.io.UI.Web.Controllers
{
    public class AgendaController : Controller
    {
        //private static DateTime _DataInicio;

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
                var usuario = _usuarioService.Obter("adfada26-37c9-44f0-8d64-fb774d9b9c83");
                //var agenda = _agendaService.Obter("35fb8c94-a7c7-488f-80db-13312e0b7cf8");

                //var eventos1 = _eventoService.Listar(agenda.Id);
                //var eventos2 = _eventoService.Listar(agenda.Id, usuario.Id);

                //var dataInicio = DateTime.Now.AddDays(-7);
                //var dataFinal = DateTime.Now;
                //var eventos3 = _eventoService.Listar(agenda.Id, dataInicio, dataFinal);

                //var local = _localService.Listar();

                var novaAgenda = new Agenda(Guid.Empty.ToString(), usuario.Id, "AGENDA ARQUITETURA NOVA");
                _agendaService.Gravar(novaAgenda);

                novaAgenda.DefinirTitulo(novaAgenda.Titulo + " - ATUALIZADA!");
                _agendaService.Gravar(novaAgenda);

                var lista = _agendaService.Listar(usuario.Id);

                //var evento = _eventoService.Obter("b4e5b559-f79a-437a-afea-9397416cd262");
                //evento.OcupaUsuario = true;
                //_eventoService.Gravar(evento);
                //GravarAgenda();
                //GravarUsuario();
                //GravarLocal();
                //GravarEvento(false);
                //GravarEvento(true);
                //GravarEvento(true, true);
                //ExcluirEvento();

                //var agendaId = "35fb8c94-a7c7-488f-80db-13312e0b7cf8";
                //var dataInicio = DateTime.Now.AddDays(-7);
                //var dataFinal = DateTime.Now;

                //var listEventos = _eventoService.ObterEventosPorPeriodo(agendaId, dataInicio, dataFinal);

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