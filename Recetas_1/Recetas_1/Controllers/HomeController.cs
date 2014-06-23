using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recetas_1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Comienza ahora mismo a compartir tus recetas con el mundo";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "¿Qué es Recetas5501?";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Ponte en contacto con nosotros";

            return View();
        }
    }
}
