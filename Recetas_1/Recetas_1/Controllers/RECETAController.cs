using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recetas_1.Models;
using System.IO;
using WebMatrix.WebData;
using System.Net.Http;
using Facebook;

namespace Recetas_1.Controllers
{   
    [Authorize]
    public class RECETAController : Controller
    {
		private readonly IRECETARepository recetaRepository;
        private readonly IROLNACIONALRepository rolNacionalRepository;
        private readonly IROLINTERNACIONALRepository rolInternacionalRepository;
        private readonly IETIQUETARepository etiquetaRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public RECETAController() : this(new RECETARepository())
        {
            this.rolNacionalRepository = new ROLNACIONALRepository();
            this.rolInternacionalRepository = new ROLINTERNACIONALRepository();
            this.etiquetaRepository = new ETIQUETARepository();
        }

        public RECETAController(IRECETARepository recetaRepository)
        {
			this.recetaRepository = recetaRepository;
            this.rolNacionalRepository = new ROLNACIONALRepository();
            this.rolInternacionalRepository = new ROLINTERNACIONALRepository();
            this.etiquetaRepository = new ETIQUETARepository();
        }

        //
        // GET: /RECETA/

        public ViewResult Index()
        {
            //Lógica de la búsqueda
            IEnumerable<RECETA> recetas = (from r in recetaRepository.All
                                           where r.USERID == WebSecurity.CurrentUserId
                                           select r);
            return View(recetas);
        }

        //
        // GET: /RECETA/Details/5

        public ViewResult Details(int id)
        {
            string campoOrigen = "Origen";
            string valorOrigen = " ";

            if (recetaRepository.Find(id).PAIS == "Costa Rica")
            {
                campoOrigen = "Provincia";
                try
                {
                    IEnumerable<string> res = (from r in rolNacionalRepository.All
                                               where r.IDRECETA == id
                                               select r.PROVINCIA);
                    valorOrigen = res.ElementAt(0);
                }
                catch { }
            }
            else
            {
                campoOrigen = "Región";
                try
                {
                    IEnumerable<string> res = (from r in rolInternacionalRepository.All
                                               where r.IDRECETA == id
                                               select r.REGION);
                    valorOrigen = res.ElementAt(0);
                }
                catch { }
            }

            ViewBag.campoOrigen = campoOrigen;
            ViewBag.valorOrigen = valorOrigen;
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
        public ActionResult Create(RECETA receta, string provincia, string region, HttpPostedFileBase fotoReceta, string etiquetas)
        {
            receta.USERID = WebSecurity.CurrentUserId;

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
                if (fotoReceta != null)
                {
                    if (fotoReceta.ContentLength > 0)
                    {
                        string nombreArchivo = Convert.ToString(idReceta) + Path.GetExtension(fotoReceta.FileName);

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

                //Lógica para insertar las etiquetas
                if (etiquetas != null)
                {
                    char[] delimitadores = { ',' };
                    string[] tags = etiquetas.Split(delimitadores);

                    if (tags.Count() > 0)
                    {
                        foreach (string tag in tags)
                        {
                            ETIQUETA nueva = new ETIQUETA();
                            nueva.IDRECETA = idReceta;
                            nueva.NOMBRE = tag.Trim();
                            etiquetaRepository.InsertOrUpdate(nueva);
                        }
                        etiquetaRepository.Save();
                    }
                }
                ///////////////////////////////////

                //try
                //{
                //    var client = new FacebookClient((string)HttpContext.Session["accesstoken"]);
                //    client.Post("/me/feed", new
                //    {
                //        message = WebSecurity.CurrentUserName +
                //                  " ha aregado una nueva receta en Recetas5501\n" +
                //                  "\tConócela en http://recetas5501.sytes.net/RECETA/Details/" +
                //                  Convert.ToString(idReceta)
                //    });
                //}
                //catch { };

                return RedirectToAction("Create", "PASO", new { idReceta = idReceta });
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
        // GET: /RECETA/Buscar

        public ViewResult Buscar()
        {
            return View();
        }

        //
        // POST: /RECETA/buscarNombre
        [HttpPost]
        public ActionResult buscarNombre(string nombre)
        {
            //Lógica de la búsqueda
            IEnumerable<RECETA> res = (from r in recetaRepository.All
                                       where r.NOMBRE.Contains(nombre)
                                       select r).AsEnumerable<RECETA>();

            return View("Results", res);
        }

        //
        // POST: /RECETA/buscarPais
        [HttpPost]
        public ActionResult buscarPais(string pais)
        {
            //Lógica de la búsqueda
            IEnumerable<RECETA> res = (from r in recetaRepository.All
                                       where r.PAIS == pais
                                       select r).AsEnumerable<RECETA>();

            return View("Results", res);
        }

        //
        // POST: /RECETA/buscarEtiqueta
        [HttpPost]
        public ActionResult buscarEtiqueta(string etiqueta)
        {
            //Lógica de la búsqueda
            IEnumerable<int> recetas = (from e in etiquetaRepository.All
                             where e.NOMBRE == etiqueta
                             select e.IDRECETA).Distinct();

            List<RECETA> resultados = new List<RECETA>();

            foreach (int id in recetas)
            {
                resultados.Add(recetaRepository.Find(id));
            }

            IEnumerable<RECETA> res = resultados.AsEnumerable();

            return View("Results", res);
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

