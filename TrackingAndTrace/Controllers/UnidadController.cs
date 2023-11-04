using System;
using System.Collections.Generic;
using System.Configuration;
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
            ML.Unidad unidades = new ML.Unidad();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44387/api/");
                var responseTask = client.GetAsync("Unidad");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Unidad>();
                    readTask.Wait();
                    unidades = readTask.Result;
                }
            }
            return View(unidades);
        }

        [HttpGet]
        public ActionResult Form(int? IdUnidad)
        {
            ML.Unidad unidad = new ML.Unidad();
            if(IdUnidad != null)
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("https://localhost:44387/api/");
                    var responseTask = cliente.GetAsync("Unidad/GetById/" + IdUnidad);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Unidad>();
                        readTask.Wait();
                        unidad = readTask.Result;
                    }

                }
            }
            return View(unidad);
        }
        /*
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
        */
        [HttpPost]
        public ActionResult Form(ML.Unidad unidad)
        {
            if (unidad.IdUnidad == 0) //ADD
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("https://localhost:44387/api/");

                    var postTask = cliente.PostAsJsonAsync("unidad/Add", unidad);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se Agrego correctamente la unidad";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo agregar la unidad";
                    }
                }
            }
            else //UPDATE
            {
                using (var cliente = new HttpClient())
                {
                    int IdUnidad = unidad.IdUnidad;

                    cliente.BaseAddress = new Uri("https://localhost:44387/api/");

                    var putTask = cliente.PutAsJsonAsync("unidad/Update/" + IdUnidad, unidad);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se actualizo correctamente la unidad";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se logro actualizar la unidad";
                    }
                }
            }
            return PartialView("Modal");
        }
        /*
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
        */
        /*
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
        */
        public ActionResult Delete(int IdUnidad)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44387/api/");

                // HTTP DELETE
                var deleteTask = client.DeleteAsync("unidad/Delete/" + IdUnidad);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Error");
        }
    }
}