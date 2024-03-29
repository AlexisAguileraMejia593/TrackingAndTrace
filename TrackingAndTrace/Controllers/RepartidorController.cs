﻿using System;
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
            ML.Repartidor repartidor = BL.Repartidor.GetAll();
            var result = repartidor.Repartidores;
            return View(repartidor);
        }
        [HttpGet]
        public ActionResult Form(int? IdRepartidor)
        {
            ML.Repartidor repartidor = new ML.Repartidor();
            repartidor.Unidad = new ML.Unidad();
            repartidor.Usuario = new ML.Usuario();
            ML.Unidad unidadobj = BL.Unidad.GetAll();
            ML.Usuario usuarioobj = BL.Usuario.GetAll();
            repartidor.Unidad.Unidades = unidadobj.Unidades;
            repartidor.Usuario.Usuarios = usuarioobj.Usuarios;

            if (IdRepartidor != null)
            {
                var list = BL.Repartidor.GetById(IdRepartidor.Value);
                if (list != null)
                {
                    //UNBOXING
                    repartidor = (ML.Repartidor)list;
                    repartidor.Unidad.Unidades = unidadobj.Unidades;
                    repartidor.Usuario.Usuarios = usuarioobj.Usuarios;
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
                    var result = BL.Repartidor.Add(repartidor);
                    if (result != null)
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
                    var result = BL.Repartidor.Update(repartidor);
                    if (result != null)
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
                ML.Unidad unidadobj = BL.Unidad.GetAll();
                ML.Usuario usuarioobj = BL.Usuario.GetAll();
                repartidor.Unidad.Unidades = unidadobj.Unidades;
                repartidor.Usuario.Usuarios = usuarioobj.Usuarios;
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
        public ActionResult UnidaddelRepartidor()
        {
            int IdUsuario = Convert.ToInt32(Session["Usuario"]);
            ML.Repartidor repartidor = BL.Repartidor.GetByIdUsuario(IdUsuario);
            var result = repartidor.Repartidores;
            return View(repartidor);
        }
    }
}