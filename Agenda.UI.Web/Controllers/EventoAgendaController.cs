using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.UI.Web.Controllers
{
    public class EventoAgendaController : Controller
    {
        // GET: EventoAgenda
        public ActionResult Index()
        {
            return View();
        }

        // GET: EventoAgenda/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventoAgenda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventoAgenda/Create
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

        // GET: EventoAgenda/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventoAgenda/Edit/5
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

        // GET: EventoAgenda/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventoAgenda/Delete/5
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