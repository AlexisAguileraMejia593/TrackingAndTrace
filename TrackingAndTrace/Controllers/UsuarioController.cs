using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TrackingAndTrace.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            ML.Usuario usuario = new ML.Usuario();
            List<ML.Usuario> list = BL.Usuario.GetAll();
            usuario.Usuarios = list;
            return View(usuario);
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginVerificacion(string email, string password)
        {
            var result =  BL.Usuario.GetByEmail(email);
            if (result != null)
            {
                ML.Usuario usuario = (ML.Usuario)result;
                if (password == usuario.Password)
                {
                    Session["Role"] = usuario.Rol.Tipo;
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    ViewBag.Mensaje = "Contraseña Incorrecta";
                    ViewBag.Correo = false;
                    return RedirectToAction("Login", "Usuario");


                }
            }
            else
            {
                ViewBag.Mensaje = "No existe la cuenta";
                ViewBag.Correo = false;
                return PartialView("Modal");
            }

        }
        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}