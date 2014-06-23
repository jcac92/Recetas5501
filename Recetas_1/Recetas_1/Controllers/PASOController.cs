using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recetas_1.Models;
using System.IO;

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

        public ViewResult Index(int idReceta)
        {
            IEnumerable<PASO> pasosReceta = (from p in pasoRepository.All
                                             where p.IDRECETA == idReceta
                                             select p);
            return View(pasosReceta);
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
        public ActionResult Create(PASO paso, HttpPostedFileBase fotoPaso, bool ultimoPaso)
        {
            if (ModelState.IsValid) {

                //Lógica para insertar el nuevo Paso
                pasoRepository.InsertOrUpdate(paso);
                pasoRepository.Save();

                //ID del paso recién insertado
                int idPaso = (from p in pasoRepository.All
                              select p.IDPASO)
                              .Max();

                //Lógica para insertar la imagen
                string nombreArchivo = Convert.ToString(idPaso) + Path.GetExtension(fotoPaso.FileName);

                if (fotoPaso != null)
                {
                    if (fotoPaso.ContentLength > 0)
                    {
                        //Subir archivo de foto
                        string rutaArchivo = Path.Combine(Server.MapPath("~/Images/Pasos"), nombreArchivo);
                        fotoPaso.SaveAs(rutaArchivo);

                        //Editar la receta guardada para agregar la ruta de la foto
                        PASO editar = pasoRepository.Find(idPaso);
                        editar.FOTO = "../../Images/Pasos/" + nombreArchivo;
                        pasoRepository.InsertOrUpdate(editar);
                        pasoRepository.Save();
                    }
                }
                ///////////////////////////////

                //Regresar a las recetas si es el último paso
                if (ultimoPaso)
                    return RedirectToAction("Index", "RECETA");
                else
                    return RedirectToAction("Create");

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

