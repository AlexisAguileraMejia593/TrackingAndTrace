using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        public ActionResult CrearPaquete(ML.Paquete paquete, string Email)
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
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(Email, "utcd alea dwje xqfe"),
                EnableSsl = true
            };
            string mMailServer = "smtp.gmail.com";
            string mTo = "5577901650@txt.att.net";
            string mFrom = Email;
            string mMsg = "Hola, este es un mensaje de texto enviado desde una aplicación C#.";
            string mSubject = "Mensaje de texto";

            // Crear y enviar el mensaje
            try
            {
                MailMessage message = new MailMessage(mFrom, mTo, mSubject, mMsg);
                SmtpClient mySmtpClient = new SmtpClient(mMailServer);
                mySmtpClient.UseDefaultCredentials = true;
                mySmtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el mensaje: " + ex.Message);
            }

            return RedirectToAction("Paquetes");
        }
    }
}