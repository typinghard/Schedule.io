using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.io.UI.Web.Models;
using Schedule.io.Interfaces.Services;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using Schedule.io.Core.Communication.Mediator;
using Schedule.io.Events.AgendaEvents;

namespace Schedule.io.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScheduleIo _scheduleIo;
        private readonly IMediatorHandler _bus;

        public HomeController(ILogger<HomeController> logger, IScheduleIo scheduleIo, IMediatorHandler bus)
        {
            _bus = bus;
            _logger = logger;
            _scheduleIo = scheduleIo;
        }

        public IActionResult Index()
        {
            _scheduleIo.Locais().Gravar(new Local("teste"));

            _bus.PublicarEvento(new AgendaAtualizadaEvent("", "", "", "", false));

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
