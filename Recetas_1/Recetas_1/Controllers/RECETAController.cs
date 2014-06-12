using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recetas_1.Models;

namespace Recetas_1.Controllers
{   
    public class RECETAController : Controller
    {
		private readonly IRECETARepository recetaRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public RECETAController() : this(new RECETARepository())
        {
        }

        public RECETAController(IRECETARepository recetaRepository)
        {
			this.recetaRepository = recetaRepository;
        }

        //
        // GET: /RECETA/

        public ViewResult Index()
        {
            return View(recetaRepository.AllIncluding(receta => receta.ETIQUETA, receta => receta.PASO));
        }

        //
        // GET: /RECETA/Details/5

        public ViewResult Details(int id)
        {
            return View(recetaRepository.Find(id));
        }

        //
        // GET: /RECETA/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /RECETA/Create

        [HttpPost]
        public ActionResult Create(RECETA receta)
        {
            if (ModelState.IsValid) {
                recetaRepository.InsertOrUpdate(receta);
                recetaRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /RECETA/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(recetaRepository.Find(id));
        }

        //
        // POST: /RECETA/Edit/5

        [HttpPost]
        public ActionResult Edit(RECETA receta)
        {
            if (ModelState.IsValid) {
                recetaRepository.InsertOrUpdate(receta);
                recetaRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /RECETA/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(recetaRepository.Find(id));
        }

        //
        // POST: /RECETA/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            recetaRepository.Delete(id);
            recetaRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                recetaRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

