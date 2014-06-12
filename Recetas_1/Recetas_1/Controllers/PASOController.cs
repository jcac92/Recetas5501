using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recetas_1.Models;

namespace Recetas_1.Controllers
{   
    public class PASOController : Controller
    {
		private readonly IPASORepository pasoRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public PASOController() : this(new PASORepository())
        {
        }

        public PASOController(IPASORepository pasoRepository)
        {
			this.pasoRepository = pasoRepository;
        }

        //
        // GET: /PASO/

        public ViewResult Index()
        {
            return View(pasoRepository.AllIncluding(paso => paso.INGREDIENTE));
        }

        //
        // GET: /PASO/Details/5

        public ViewResult Details(int id)
        {
            return View(pasoRepository.Find(id));
        }

        //
        // GET: /PASO/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /PASO/Create

        [HttpPost]
        public ActionResult Create(PASO paso)
        {
            if (ModelState.IsValid) {
                pasoRepository.InsertOrUpdate(paso);
                pasoRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /PASO/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(pasoRepository.Find(id));
        }

        //
        // POST: /PASO/Edit/5

        [HttpPost]
        public ActionResult Edit(PASO paso)
        {
            if (ModelState.IsValid) {
                pasoRepository.InsertOrUpdate(paso);
                pasoRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /PASO/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(pasoRepository.Find(id));
        }

        //
        // POST: /PASO/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            pasoRepository.Delete(id);
            pasoRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                pasoRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

