using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recetas_1.Models;
using System.IO;

namespace Recetas_1.Controllers
{   
    public class RECETAController : Controller
    {
		private readonly IRECETARepository recetaRepository;
        private readonly IROLNACIONALRepository rolNacionalRepository;
        private readonly IROLINTERNACIONALRepository rolInternacionalRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public RECETAController() : this(new RECETARepository())
        {
            this.rolNacionalRepository = new ROLNACIONALRepository();
            this.rolInternacionalRepository = new ROLINTERNACIONALRepository();
        }

        public RECETAController(IRECETARepository recetaRepository)
        {
			this.recetaRepository = recetaRepository;
            this.rolNacionalRepository = new ROLNACIONALRepository();
            this.rolInternacionalRepository = new ROLINTERNACIONALRepository();
        }

        //
        // GET: /RECETA/

        public ViewResult Index()
        {
            //return View(recetaRepository.AllIncluding(receta => receta.ETIQUETA, receta => receta.PASO));
            return View(recetaRepository.All);
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
            RECETA nueva = new RECETA();
            nueva.PAIS = "Costa Rica";
            return View(nueva);
        } 

        //
        // POST: /RECETA/Create

        [HttpPost]
        public ActionResult Create(RECETA receta, string provincia, string region, HttpPostedFileBase fotoReceta)
        {
            if (ModelState.IsValid) {

                //Lógica para insertar la nueva receta
                recetaRepository.InsertOrUpdate(receta);
                recetaRepository.Save();

                //ID de la receta recién insertada
                int idReceta = (from r in recetaRepository.All
                                select r.IDRECETA)
                                .Max();

                //Lógica para asignar el RolOrigen. Se utiliza el patrón Jugador-Rol
                if (receta.PAIS == "Costa Rica")
                {
                    rolNacionalRepository.InsertOrUpdate(new ROLNACIONAL(default(int), idReceta, provincia));
                    rolNacionalRepository.Save();
                }
                else
                {
                    rolInternacionalRepository.InsertOrUpdate(new ROLINTERNACIONAL(default(int), idReceta, region));
                    rolInternacionalRepository.Save();
                }
                ////////////////////////////////////////////////////////////////////

                //Lógica para insertar la imagen
                string nombreArchivo = Convert.ToString(idReceta) + Path.GetExtension(fotoReceta.FileName);

                if (fotoReceta != null)
                {
                    if (fotoReceta.ContentLength > 0)
                    {
                        //Subir archivo de foto
                        string rutaArchivo = Path.Combine(Server.MapPath("~/Images/Recetas"), nombreArchivo);
                        fotoReceta.SaveAs(rutaArchivo);

                        //Editar la receta guardada para agregar la ruta de la foto
                        RECETA editar = recetaRepository.Find(idReceta);
                        editar.FOTO = "../../Images/Recetas/"+nombreArchivo;
                        recetaRepository.InsertOrUpdate(editar);
                        recetaRepository.Save();

                    }
                }
                ///////////////////////////////

                return RedirectToAction("Create", "PASO");
            } else {
				return View();
			}
        }

        //
        // GET: /RECETA/Pasos

        public ActionResult Pasos(int id)
        {
            return RedirectToAction("Index", "PASO", new { idReceta=id });
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

