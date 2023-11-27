using ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Colors;

namespace TrackingAndTrace.Controllers
{
    public class EntregaController : Controller
    {
        // GET: Paquete
        public ActionResult Index()
        {
            string detalle = "";
            string nombre = "";
            ML.Entrega entrega = new ML.Entrega();
            var result = BL.Entrega.GetAll(detalle, nombre);
            entrega.Entregas = result.Entregas;
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
            string detalle = "";
            string nombre = "";
            ML.Entrega entrega = new ML.Entrega();
            var result = BL.Entrega.GetAll(detalle, nombre);
            entrega.Entregas = result.Entregas;
            return View(entrega);
        }
        [HttpPost]
        public ActionResult CrearPaquete(ML.Paquete paquete, string Email, string FormId, ML.Entrega entrega)
        {
            if(FormId == "FormularioFiltrado")
            {
                string detalle = entrega.Paquete.Detalle;
                string nombre = entrega.Repartidor.Usuario.Nombre;

                var result = BL.Entrega.GetAll(detalle, nombre);

                entrega.Entregas = result.Entregas;
                return View(entrega);
            } else if (FormId == "FormularioCrearPaquete")
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
                    return RedirectToAction("Paquetes");
                }
                else
                {
                    ViewBag.Mensaje = "No se ha ingresado correctamente el paquete. Error: " + result;
                }
                return View();
            }
            return View();
        }
        public ActionResult GenerarPDF()
        {
            string detalle = "";
            string nombre = "";
            ML.Entrega entrega = new ML.Entrega();
            var result = BL.Entrega.GetAll(detalle, nombre);
            entrega.Entregas = result.Entregas;

            // Crear un nuevo documento PDF en una ubicación temporal
            string rutaTempPDF = Path.GetTempFileName() + ".pdf";

            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(rutaTempPDF)))
            {
                using (Document document = new Document(pdfDocument))
                {
                    document.Add(new Paragraph("Resumen de Compra"));

                    // Crear la tabla para mostrar la lista de objetos
                    iText.Layout.Element.Table table = new iText.Layout.Element.Table(5); // 5 columnas
                    table.SetWidth(UnitValue.CreatePercentValue(100)); // Ancho de la tabla al 100% del documento

                    // Establecer el color de fondo de la tabla
                    table.SetBackgroundColor(ColorConstants.LIGHT_GRAY);

                    // Añadir las celdas de encabezado a la tabla
                    table.AddHeaderCell("ID Entrega");
                    table.AddHeaderCell("ID Paquete");
                    table.AddHeaderCell("Detalle");
                    table.AddHeaderCell("Peso");
                    table.AddHeaderCell("Direccion de Origen");

                    // Añadir las celdas de datos a la tabla
                    foreach (ML.Entrega entrega1 in entrega.Entregas)
                    {
                        table.AddCell(entrega1.IdEntrega.ToString());
                        table.AddCell(entrega1.Paquete.IdPaquete.ToString());
                        table.AddCell(entrega1.Paquete.Detalle.ToString());
                        table.AddCell(entrega1.Paquete.Peso.ToString());
                        table.AddCell(entrega1.Paquete.DireccionOrigen.ToString());
                    }

                    // Añadir la tabla al documento
                    document.Add(table);
                }
            }

            // Leer el archivo PDF como un arreglo de bytes
            byte[] fileBytes = System.IO.File.ReadAllBytes(rutaTempPDF);

            // Eliminar el archivo temporal
            System.IO.File.Delete(rutaTempPDF);
            HttpContext.Session.Remove("Carrito");

            // Descargar el archivo PDF
            return new FileStreamResult(new MemoryStream(fileBytes), "application/pdf")
            {
                FileDownloadName = "ReporteProductos.pdf"
            };
        }
    }
}