using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Agenda.UI.Web.Models;
using Schedule.io.Interfaces;

namespace Agenda.UI.Web.Controllers
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
            _scheduleIo.Agendas().Gravar(new Schedule.io.Models.Agenda()
            {
                Titulo = "DINHEIROOO"
            });

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
