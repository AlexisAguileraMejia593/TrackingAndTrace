using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackingAndTrace.Controllers
{
    public class RepartidorController : Controller
    {
        // GET: Repartidor
        [HttpGet]
        public ActionResult Index()
        {
            ML.Repartidor repartidor = new ML.Repartidor();
            List<ML.Repartidor> list = BL.Repartidor.GetAll();
            repartidor.Repartidores = list;
            return View(repartidor);
        }
        [HttpGet]
        public ActionResult Form(int? IdRepartidor)
        {
            ML.Repartidor repartidor = new ML.Repartidor();

            if (IdRepartidor != null)
            {
                var list = BL.Repartidor.GetById(IdRepartidor.Value);
                if (list != null)
                {
                    //UNBOXING
                    repartidor = (ML.Repartidor)list;
                }
            }
            else
            {

            }

            return View(repartidor);
        }
        [HttpPost]
        public ActionResult Form(ML.Repartidor repartidor)
        {
            if (ModelState.IsValid)
            {
                if (repartidor.IdRepartidor == 0)
                {
                    bool result = BL.Repartidor.Add(repartidor);
                    if (result)
                    {
                        ViewBag.Mensaje = "Se ha ingresado correctamente el repartidor";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha ingresado correctamente el repartidor. Error: " + result;
                    }
                }
                else
                {
                    bool result = BL.Repartidor.Update(repartidor);
                    if (result)
                    {
                        ViewBag.Mensaje = "se ha actualizado correctamente el repartidor";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha podido actualizar correctamente el repartidor. Error: " + result;
                    }
                }
            }
            else
            {
                return View(repartidor);
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int? IdRepartidor)
        {
            bool result = BL.Repartidor.Delete(IdRepartidor.Value);
            if (result)
            {
                ViewBag.Mensaje = "Se ha eliminado correctamente el registro";
            }
            else
            {
                ViewBag.Mensaje = "NO se ha eliminado correctamente el registro. Error: " + result;
            }
            return PartialView("Modal");
        }
    }
}