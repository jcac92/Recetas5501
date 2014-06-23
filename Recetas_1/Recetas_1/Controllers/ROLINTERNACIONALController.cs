using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recetas_1.Models;

namespace Recetas_1.Controllers
{   
    public class ROLINTERNACIONALController : Controller
    {
		private readonly IROLINTERNACIONALRepository rolinternacionalRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ROLINTERNACIONALController() : this(new ROLINTERNACIONALRepository())
        {
        }

        public ROLINTERNACIONALController(IROLINTERNACIONALRepository rolinternacionalRepository)
        {
			this.rolinternacionalRepository = rolinternacionalRepository;
        }

        //
        // GET: /ROLINTERNACIONAL/

        public ViewResult Index()
        {
            return View(rolinternacionalRepository.All);
        }

        //
        // GET: /ROLINTERNACIONAL/Details/5

        public ViewResult Details(int id)
        {
            return View(rolinternacionalRepository.Find(id));
        }

        //
        // GET: /ROLINTERNACIONAL/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ROLINTERNACIONAL/Create

        [HttpPost]
        public ActionResult Create(ROLINTERNACIONAL rolinternacional)
        {
            if (ModelState.IsValid) {
                rolinternacionalRepository.InsertOrUpdate(rolinternacional);
                rolinternacionalRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /ROLINTERNACIONAL/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(rolinternacionalRepository.Find(id));
        }

        //
        // POST: /ROLINTERNACIONAL/Edit/5

        [HttpPost]
        public ActionResult Edit(ROLINTERNACIONAL rolinternacional)
        {
            if (ModelState.IsValid) {
                rolinternacionalRepository.InsertOrUpdate(rolinternacional);
                rolinternacionalRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /ROLINTERNACIONAL/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(rolinternacionalRepository.Find(id));
        }

        //
        // POST: /ROLINTERNACIONAL/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            rolinternacionalRepository.Delete(id);
            rolinternacionalRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                rolinternacionalRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

