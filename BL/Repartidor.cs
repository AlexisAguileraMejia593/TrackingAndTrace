using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Repartidor
    {
        public static ML.Repartidor GetAll()
        {
            ML.Repartidor repartidorobject = new ML.Repartidor();
            repartidorobject.Repartidores = new List<ML.Repartidor>();
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var query = (from repart in context.Repartidor
                                 select new
                                 {
                                     IdRepartidor = repart.IdRepartidor,
                                     IdUnidadAsignada = repart.Unidad.IdUnidad,
                                     Telefono = repart.Telefono,
                                     FechaIngreso = repart.FechaIngreso,
                                     Fotografia = repart.Fotografia,
                                     IdUsuario = repart.Usuario.IdUsuario
                                 });
                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            ML.Repartidor repartidor = new ML.Repartidor();
                            repartidor.IdRepartidor = registro.IdRepartidor;
                            repartidor.Unidad = new ML.Unidad();
                            repartidor.Unidad.IdUnidad = registro.IdUnidadAsignada;
                            repartidor.Telefono = registro.Telefono.Value;
                            repartidor.FechaIngreso = registro.FechaIngreso.Value;
                            repartidor.Fotografia = registro.Fotografia;
                            repartidor.Usuario = new ML.Usuario();
                            repartidor.Usuario.IdUsuario = registro.IdUsuario;    
                            repartidorobject.Repartidores.Add(repartidor);
                        }
                    }
                    else 
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return repartidorobject;
        }
        public static ML.Repartidor Add(ML.Repartidor repartidor)
        {
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    DL.Repartidor repartidorEntity = new DL.Repartidor();

                    DL.Unidad unidadEntity = context.Unidad.Find(repartidor.Unidad.IdUnidad);
                    if (unidadEntity == null)
                    {
                        return null;
                    }

                    repartidorEntity.Unidad = unidadEntity;
                    repartidorEntity.Telefono = repartidor.Telefono;
                    repartidorEntity.FechaIngreso = repartidor.FechaIngreso;
                    repartidorEntity.Fotografia = repartidor.Fotografia;
                    repartidorEntity.IdUsuario = repartidor.Usuario.IdUsuario;

                    context.Repartidor.Add(repartidorEntity);
                    int rowsAffected = context.SaveChanges();

                    if (rowsAffected > 0)
                    {
                        repartidor.IdRepartidor = repartidorEntity.IdRepartidor;
                        return repartidor;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static ML.Repartidor GetById(int IdRepartidor)
        {
            ML.Repartidor result = null;
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    DL.Repartidor repartidorEntity = new DL.Repartidor();
                    var query = (from repart in context.Repartidor
                                 where IdRepartidor == repart.IdRepartidor
                                 select new
                                 {
                                     IdRepartidor = repart.IdRepartidor,
                                     IdUnidadAsignada = repart.Unidad.IdUnidad,
                                     Telefono = repart.Telefono,
                                     FechaIngreso = repart.FechaIngreso,
                                     Fotografia = repart.Fotografia,
                                     IdUsuario = repart.Usuario.IdUsuario
                                 });
                    if (query != null)
                    {
                        List<ML.Repartidor> list = new List<ML.Repartidor>();
                        foreach (var registro in query)
                        {
                            ML.Repartidor repartidorlist = new ML.Repartidor();
                            repartidorlist.IdRepartidor = registro.IdRepartidor;
                            repartidorlist.Unidad = new ML.Unidad();
                            repartidorlist.Unidad.IdUnidad = registro.IdUnidadAsignada;
                            repartidorlist.Telefono = registro.Telefono.Value;
                            repartidorlist.FechaIngreso = registro.FechaIngreso.Value;
                            repartidorlist.Fotografia = registro.Fotografia;
                            repartidorlist.Usuario = new ML.Usuario();
                            repartidorlist.Usuario.IdUsuario = registro.IdUsuario;
                            // Boxing
                            object boxedRepartidor = repartidorlist;
                            list.Add(repartidorlist);
                        }
                        result = list.FirstOrDefault();
                    }
                }
            }
            catch
            {
                // Handle exception
            }
            return result;
        }
        public static bool Update(ML.Repartidor repartidor)
        {
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var query = context.Repartidor.SingleOrDefault(r => r.IdRepartidor == repartidor.IdRepartidor);
                    if (query != null)
                    {
                        // Busca la Unidad existente en la base de datos
                        DL.Unidad unidadEntity = context.Unidad.Find(repartidor.Unidad.IdUnidad);
                        if (unidadEntity == null)
                        {
                            // La Unidad no existe en la base de datos
                            return false;
                        }

                        // Asigna la Unidad existente a query
                        query.Unidad = unidadEntity;

                        // Busca el Usuario existente en la base de datos
                        DL.Usuario usuarioEntity = context.Usuario.Find(repartidor.Usuario.IdUsuario);
                        if (usuarioEntity == null)
                        {
                            // El Usuario no existe en la base de datos
                            return false;
                        }

                        // Asigna el Usuario existente a query
                        query.Usuario = usuarioEntity;

                        query.Telefono = repartidor.Telefono;
                        query.FechaIngreso = repartidor.FechaIngreso;
                        query.Fotografia = repartidor.Fotografia;

                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(int IdRepartidor)
        {
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var query = (from a in context.Repartidor
                                 where a.IdRepartidor == IdRepartidor
                                 select a).First();
                    context.Repartidor.Remove(query);
                    int rowAffected = context.SaveChanges();
                    if (rowAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static ML.Repartidor GetByIdUsuario(int IdUsuario)
        {
            ML.Repartidor result = null;
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var query = (from repartidor in context.Repartidor
                                  join unidadEntrega in context.Unidad on repartidor.Unidad.IdUnidad equals unidadEntrega.IdUnidad
                                  join usuario in context.Usuario on repartidor.IdUsuario equals usuario.IdUsuario
                                  join estatusUnidad in context.EstatusUnidad on unidadEntrega.IdEstatusUnidad equals estatusUnidad.IdEstatus
                                  where usuario.IdUsuario == IdUsuario
                                  select new
                                  {
                                      IdRepartidor = repartidor.IdRepartidor,
                                      IdUnidadAsignada = repartidor.Unidad.IdUnidad,
                                      Telefono = repartidor.Telefono,
                                      FechaIngreso = repartidor.FechaIngreso,
                                      Fotografia = repartidor.Fotografia,
                                      IdUsuario = usuario.IdUsuario,
                                      UserName = usuario.UserName,
                                      IdRol = usuario.IdRol,
                                      Email = usuario.Email,
                                      Nombre = usuario.Nombre,
                                      ApellidoPaterno = usuario.ApellidoPaterno,
                                      ApellidoMaterno = usuario.ApellidoMaterno,
                                      Password = usuario.Password,
                                      IdUnidad = unidadEntrega.IdUnidad,
                                      NumeroPlaca = unidadEntrega.NumeroPlaca,
                                      Modelo = unidadEntrega.Modelo,
                                      Marca = unidadEntrega.Marca,
                                      AnoFabricacion = unidadEntrega.AnoFabricacion,
                                      IdEstatus = estatusUnidad.IdEstatus,
                                      Estatus = estatusUnidad.Estatus
                                  }).ToList();
                    if (query != null)
                    {
                        List<ML.Repartidor> list = new List<ML.Repartidor>();
                        foreach (var registro in query)
                        {
                            ML.Repartidor repartidorlist = new ML.Repartidor();
                            repartidorlist.Telefono = registro.Telefono.Value;
                            repartidorlist.FechaIngreso = registro.FechaIngreso.Value;
                            repartidorlist.Fotografia = registro.Fotografia;
                            repartidorlist.IdRepartidor = registro.IdRepartidor;
                            repartidorlist.Unidad = new ML.Unidad();
                            repartidorlist.Unidad.IdUnidad = registro.IdUnidad;
                            repartidorlist.Unidad.NumeroPlaca = registro.NumeroPlaca;
                            repartidorlist.Unidad.Modelo = registro.Modelo;
                            repartidorlist.Unidad.Marca = registro.Marca;
                            repartidorlist.Unidad.AñoFabricacion = registro.AnoFabricacion;
                            repartidorlist.Usuario = new ML.Usuario();
                            repartidorlist.Usuario.IdUsuario = registro.IdUsuario;
                            repartidorlist.Usuario.UserName = registro.UserName;
                            repartidorlist.Usuario.Nombre = registro.Nombre;
                            repartidorlist.Usuario.ApellidoPaterno = registro.ApellidoPaterno;
                            repartidorlist.Usuario.ApellidoMaterno = registro.ApellidoMaterno;
                            repartidorlist.Unidad.EstatusUnidad = new ML.EstatusUnidad();
                            repartidorlist.Unidad.EstatusUnidad.IdEstatus = registro.IdEstatus;
                            repartidorlist.Unidad.EstatusUnidad.Estatus = registro.Estatus;
                            // Boxing
                            object boxedRepartidor = repartidorlist;
                            list.Add(repartidorlist);
                        }
                        result = list.FirstOrDefault();
                    }
                }
            }
            catch
            {
                // Handle exception
            }
            return result;
        }
    }
}