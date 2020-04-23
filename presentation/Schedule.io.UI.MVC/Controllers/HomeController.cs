using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.io.UI.Web.Models;
using Schedule.io.Interfaces.Services;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;

namespace Schedule.io.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScheduleIo _scheduleIo;

        public HomeController(ILogger<HomeController> logger, IScheduleIo scheduleIo)
        {
            _logger = logger;
            _scheduleIo = scheduleIo;
        }

        public IActionResult Index()
        {
            var usuario1 = _scheduleIo.Usuarios().Obter("2ab976ef-e1d7-4557-9a2e-e5f4a8511f3f");
            var usuario2 = _scheduleIo.Usuarios().Obter("5177091d-0773-4efb-960f-758ba459b28b");
            var usuario3 = _scheduleIo.Usuarios().Obter("63120bbe-1857-46ad-abaf-1a063a8fc65d");

            var agendaUsuario2 = new AgendaUsuario(usuario2.Id);
            var agendaUsuario3 = new AgendaUsuario(usuario3.Id);

            var agenda = new Agenda(usuario1.Id, "Teste 1");
            agenda.AdicionarUsuario(agendaUsuario2);
            agenda.AdicionarUsuario(agendaUsuario3);
            _scheduleIo.Agendas().Gravar(agenda);

            agenda.RemoverUsuario(agendaUsuario2);

            var usuario4 = _scheduleIo.Usuarios().Gravar("chapolin@mail.com");
            var agendaUsuario4 = new AgendaUsuario(usuario4.Id);
            agenda.AdicionarUsuario(agendaUsuario4);

            _scheduleIo.Agendas().Gravar(agenda);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
