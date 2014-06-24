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
        private readonly IRECETARepository recetaRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public PASOController() : this(new PASORepository())
        {
            this.recetaRepository = new RECETARepository();
        }

        public PASOController(IPASORepository pasoRepository)
        {
			this.pasoRepository = pasoRepository;
            this.recetaRepository = new RECETARepository();
        }

        //
        // GET: /PASO/

        public ViewResult Index(int idReceta)
        {
            string nombreReceta = recetaRepository.Find(idReceta).NOMBRE;

            IEnumerable<PASO> pasosReceta = (from p in pasoRepository.All
                                             where p.IDRECETA == idReceta
                                             select p);
            ViewBag.idReceta = idReceta;
            ViewBag.nombreReceta = nombreReceta;
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

        public ActionResult Create(int idReceta)
        {
            PASO nuevo = new PASO();
            nuevo.IDRECETA = idReceta;
            return View(nuevo);
        } 

        //
        // POST: /PASO/Create

        [HttpPost]
        public ActionResult Create(PASO paso, HttpPostedFileBase fotoPaso)
        {
            if (ModelState.IsValid) {

                //Variable para el número de paso
                int num = 0;
                try
                {
                    //Num último paso insertado
                    num = (from p in pasoRepository.All
                           where p.IDRECETA == paso.IDRECETA
                           select p.NUMEROPASO)
                          .Max();
                }
                catch { }
                paso.NUMEROPASO = num + 1;

                //Lógica para insertar el nuevo Paso
                pasoRepository.InsertOrUpdate(paso);
                pasoRepository.Save();

                //ID del paso recién insertado
                int idPaso = (from p in pasoRepository.All
                              select p.IDPASO)
                              .Max();

                //Lógica para insertar la imagen
                if (fotoPaso != null)
                {
                    if (fotoPaso.ContentLength > 0)
                    {
                        string nombreArchivo = Convert.ToString(idPaso) + Path.GetExtension(fotoPaso.FileName);

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

                return RedirectToAction("Create", "INGREDIENTE", new { idPaso = idPaso });

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
                return RedirectToAction("Index", new { idReceta = paso.IDRECETA });
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
            //ID de la receta a la que pertenecía el Paso a eliminar
            int idReceta = pasoRepository.Find(id).IDRECETA;

            //Lógica para eliminar el Paso
            pasoRepository.Delete(id);
            pasoRepository.Save();

            //Regresa a la lista de Pasos de la Recetaoriginal
            return RedirectToAction("Index", new { idReceta = idReceta });
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

