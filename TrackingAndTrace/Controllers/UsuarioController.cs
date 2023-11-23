using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TrackingAndTrace.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult Index()
        {
            ML.Usuario usuario = BL.Usuario.GetAll();
            var result = usuario.Usuarios;
            return View(usuario);
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
                usuario.Rol = new ML.Rol();
                ML.Rol rolObj = BL.Rol.GetAll();
                usuario.Rol.Roles = rolObj.Roles;

            if (IdUsuario != null)
                {
                    var result = BL.Usuario.GetById(IdUsuario.Value);
                    if (result != null)
                    {
                    //UNBOXING
                    usuario = (ML.Usuario)result;
                    usuario.Rol.Roles = rolObj.Roles;
                }

            }
           return View(usuario);         
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.IdUsuario == 0)
                {
                    var result = BL.Usuario.Add(usuario);
                    if (result != null)
                    {
                        ViewBag.Mensaje = "Se ha ingresado correctamente el usuario";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha ingresado correctamente el usuario. Error: " + result;
                    }
                }
                else
                {
                    bool result = BL.Usuario.Update(usuario);
                    if (result)
                    {
                        ViewBag.Mensaje = "se ha actualizado correctamente el usuario";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha podido actualizar correctamente el usuario. Error: " + result;
                    }
                }
            }
            else
            {
                ML.Rol rolObj = BL.Rol.GetAll();
                usuario.Rol.Roles = rolObj.Roles;
                return View(usuario);
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int? IdUsuario)
        {
            bool result = BL.Usuario.Delete(IdUsuario.Value);
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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginVerificacion(string email, string password)
        {
            var result = BL.Usuario.GetByEmail(email);

            if (result != null)
            {
                ML.Usuario usuario = (ML.Usuario)result;
                string hashedPassword = EncryptPassword(password);
                if (hashedPassword == usuario.Password)
                {
                    Session["Role"] = usuario.Rol.Tipo;
                    Session["Usuario"] = usuario.IdUsuario;
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