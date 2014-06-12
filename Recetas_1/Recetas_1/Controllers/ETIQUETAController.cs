using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recetas_1.Models;

namespace Recetas_1.Controllers
{   
    public class ETIQUETAController : Controller
    {
		private readonly IETIQUETARepository etiquetaRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ETIQUETAController() : this(new ETIQUETARepository())
        {
        }

        public ETIQUETAController(IETIQUETARepository etiquetaRepository)
        {
			this.etiquetaRepository = etiquetaRepository;
        }

        //
        // GET: /ETIQUETA/

        public ViewResult Index()
        {
            return View(etiquetaRepository.All);
        }

        //
        // GET: /ETIQUETA/Details/5

        public ViewResult Details(int id)
        {
            return View(etiquetaRepository.Find(id));
        }

        //
        // GET: /ETIQUETA/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ETIQUETA/Create

        [HttpPost]
        public ActionResult Create(ETIQUETA etiqueta)
        {
            if (ModelState.IsValid) {
                etiquetaRepository.InsertOrUpdate(etiqueta);
                etiquetaRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /ETIQUETA/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(etiquetaRepository.Find(id));
        }

        //
        // POST: /ETIQUETA/Edit/5

        [HttpPost]
        public ActionResult Edit(ETIQUETA etiqueta)
        {
            if (ModelState.IsValid) {
                etiquetaRepository.InsertOrUpdate(etiqueta);
                etiquetaRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /ETIQUETA/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(etiquetaRepository.Find(id));
        }

        //
        // POST: /ETIQUETA/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            etiquetaRepository.Delete(id);
            etiquetaRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                etiquetaRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

