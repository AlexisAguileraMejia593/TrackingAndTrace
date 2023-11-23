using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackingAndTrace.Controllers
{
    public class EntregaController : Controller
    {
        // GET: Paquete
        public ActionResult Index()
        {
            ML.Entrega repartidor = BL.Entrega.GetAll();
            var result = repartidor.Entregas;
            return View(repartidor);
        }

        public ActionResult Rastrear(string codigoRastreo)
        {
            var result = BL.Entrega.GetByCodigo(codigoRastreo);
            if (result != null)
            {
                return View("Index", result);
            }
            else
            {
                TempData["AlertMessage"] = "El código de rastreo no existe.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Paquetes()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CrearPaquete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CrearPaquete(ML.Paquete paquete)
        {
            var result = BL.Paquetes.Add(paquete);
            if (result != null)
            {
                ViewBag.Mensaje = "Se ha ingresado correctamente el paquete";
            }
            else
            {
                ViewBag.Mensaje = "No se ha ingresado correctamente el paquete. Error: " + result;
            }
            return View();
        }
    }
}