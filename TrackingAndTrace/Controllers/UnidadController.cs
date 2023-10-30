using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace TrackingAndTrace.Controllers
{
    public class UnidadController : Controller
    {
        /*
        // GET: Unidad
         public ActionResult Index()
         {
             ML.Unidad unidad = new ML.Unidad();
             List<ML.Unidad> list = BL.Unidad.GetAll();
             unidad.Unidades = list;
             return View(unidad);
         }
        */

        public ActionResult Index()
        {
            List<ML.Unidad> unidades = new List<ML.Unidad>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44387/api/");
                var responseTask = client.GetAsync("Unidad");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ML.Unidad>>();
                    readTask.Wait();
                    unidades = readTask.Result.ToList();
                }
            }
            return View(unidades);
        }

        [HttpGet]
        public ActionResult Form(int? IdUnidad)
        {
            ML.Unidad unidad = new ML.Unidad();

            if (IdUnidad != null)
            {
                var list = BL.Unidad.GetById(IdUnidad.Value);
                if (list != null)
                {
                    //UNBOXING
                    unidad = (ML.Unidad)list;
                }
            }
            else
            {

            }

            return View(unidad);
        }
        [HttpPost]
        public ActionResult Form(ML.Unidad unidad)
        {
            if (ModelState.IsValid)
            {
                if (unidad.IdUnidad == 0)
                {
                    bool result = BL.Unidad.Add(unidad);
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
                    bool result = BL.Unidad.Update(unidad);
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
                return View(unidad);
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int? IdUnidad)
        {
            var result = BL.Unidad.Delete(IdUnidad.Value);
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