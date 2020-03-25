using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleIo.Nuget.Interfaces;

namespace Agenda.UI.Web.Controllers
{
    public class AgendaController : Controller
    {
        private readonly IScheduleIo _scheduleIo;
        public AgendaController(IScheduleIo scheduleIo)
        {
            _scheduleIo = scheduleIo;
        }
        // GET: Agenda
        public ActionResult Index()
        {
            _scheduleIo.Agendas().Criar(new ScheduleIo.Nuget.Models.Agenda()
            {
                Id = Guid.NewGuid().ToString()
            });

            return View();
        }

        // GET: Agenda/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Agenda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agenda/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Agenda/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Agenda/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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