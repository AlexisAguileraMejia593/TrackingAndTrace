using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackingAndTrace.Controllers
{
    public class UnidadController : Controller
    {
        // GET: Unidad
        public ActionResult Index()
        {
            ML.Unidad unidad = new ML.Unidad();
            List<ML.Unidad> list = BL.Unidad.GetAll();
            unidad.Unidades = list;
            return View(unidad);
        }
    }
}