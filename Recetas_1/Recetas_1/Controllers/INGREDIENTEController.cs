using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recetas_1.Models;

namespace Recetas_1.Controllers
{   
    public class INGREDIENTEController : Controller
    {
		private readonly IINGREDIENTERepository ingredienteRepository;
        private readonly IPASORepository pasoRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public INGREDIENTEController() : this(new INGREDIENTERepository())
        {
            this.pasoRepository = new PASORepository();
        }

        public INGREDIENTEController(IINGREDIENTERepository ingredienteRepository)
        {
			this.ingredienteRepository = ingredienteRepository;
            this.pasoRepository = new PASORepository();
        }

        //
        // GET: /INGREDIENTE/

        public ViewResult Index()
        {
            return View(ingredienteRepository.All);
        }

        //
        // GET: /INGREDIENTE/Details/5

        public ViewResult Details(int id)
        {
            return View(ingredienteRepository.Find(id));
        }

        //
        // GET: /INGREDIENTE/Create

        public ActionResult Create(int idPaso)
        {
            ViewBag.idPaso = idPaso;
            return View();
        } 

        //
        // POST: /INGREDIENTE/Create

        [HttpPost]
        public ActionResult Create(INGREDIENTE ingrediente)
        {
            if (ModelState.IsValid) {
                ingredienteRepository.InsertOrUpdate(ingrediente);
                ingredienteRepository.Save();
                return RedirectToAction("Create", new { idPaso = ingrediente.IDPASO });
            } else {
				return View();
			}
        }
        
        //
        // GET: /INGREDIENTE/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(ingredienteRepository.Find(id));
        }

        //
        // POST: /INGREDIENTE/Edit/5

        [HttpPost]
        public ActionResult Edit(INGREDIENTE ingrediente)
        {
            if (ModelState.IsValid) {
                ingredienteRepository.InsertOrUpdate(ingrediente);
                ingredienteRepository.Save();

                return RedirectToAction("Details", "PASO", new { id = ingrediente.IDPASO });
            } else {
				return View();
			}
        }

        //
        // GET: /INGREDIENTE/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(ingredienteRepository.Find(id));
        }

        //
        // POST: /INGREDIENTE/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            int idPaso = ingredienteRepository.Find(id).IDPASO;

            ingredienteRepository.Delete(id);
            ingredienteRepository.Save();

            return RedirectToAction("Details", "PASO", new { id = idPaso });
        }

        //
        // GET: /INGREDIENTE/siguientePaso

        public ActionResult siguientePaso(int idPaso)
        {
            PASO estePaso = pasoRepository.Find(idPaso);
            return RedirectToAction("Create", "PASO", new { idReceta = estePaso.IDRECETA });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                ingredienteRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

