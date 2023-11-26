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
            ML.Entrega entrega = BL.Entrega.GetAll();
            var result = entrega.Entregas;
            return View(entrega);
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

            ML.Entrega entrega = BL.Entrega.GetAll();
            var result = entrega.Entregas;

            return View(entrega);
        }
        [HttpPost]
        public ActionResult CrearPaquete(ML.Paquete paquete, string Email)
        {
            var result = BL.Paquetes.Add(paquete);
            if (result != null)
            {
                ViewBag.Mensaje = "Se ha ingresado correctamente el paquete";
                /*       SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
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
                */
                string emailOrigen = "alexaguilera992000@gmail.com";

                MailMessage mailMessage = new MailMessage
           (emailOrigen, Email, "CrearPaquete", "<p>Confirmacion de envio del paquete</p>");

                mailMessage.IsBodyHtml = true;

                string url = "https://localhost:44383/Entrega/CrearPaquete/ " + System.Web.HttpUtility.UrlEncode(Email);

                mailMessage.Body = mailMessage.Body.Replace("{Url}", url);
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, "utcd alea dwje xqfe");

                smtpClient.Send(mailMessage);
                smtpClient.Dispose();

                ViewBag.Modal = "show";
                ViewBag.Mensaje = "Se ha enviado un correo de confirmacion a tu correo electronico";
                return View();
            }
            else
            {
                ViewBag.Mensaje = "No se ha ingresado correctamente el paquete. Error: " + result;
            }
            return View();
        }
    }
}