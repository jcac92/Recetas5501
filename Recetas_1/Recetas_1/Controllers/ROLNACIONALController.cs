using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recetas_1.Models;

namespace Recetas_1.Controllers
{   
    public class ROLNACIONALController : Controller
    {
		private readonly IROLNACIONALRepository rolnacionalRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ROLNACIONALController() : this(new ROLNACIONALRepository())
        {
        }

        public ROLNACIONALController(IROLNACIONALRepository rolnacionalRepository)
        {
			this.rolnacionalRepository = rolnacionalRepository;
        }

        //
        // GET: /ROLNACIONAL/

        public ViewResult Index()
        {
            return View(rolnacionalRepository.All);
        }

        //
        // GET: /ROLNACIONAL/Details/5

        public ViewResult Details(int id)
        {
            return View(rolnacionalRepository.Find(id));
        }

        //
        // GET: /ROLNACIONAL/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ROLNACIONAL/Create

        [HttpPost]
        public ActionResult Create(ROLNACIONAL rolnacional)
        {
            if (ModelState.IsValid) {
                rolnacionalRepository.InsertOrUpdate(rolnacional);
                rolnacionalRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /ROLNACIONAL/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(rolnacionalRepository.Find(id));
        }

        //
        // POST: /ROLNACIONAL/Edit/5

        [HttpPost]
        public ActionResult Edit(ROLNACIONAL rolnacional)
        {
            if (ModelState.IsValid) {
                rolnacionalRepository.InsertOrUpdate(rolnacional);
                rolnacionalRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /ROLNACIONAL/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(rolnacionalRepository.Find(id));
        }

        //
        // POST: /ROLNACIONAL/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            rolnacionalRepository.Delete(id);
            rolnacionalRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                rolnacionalRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

