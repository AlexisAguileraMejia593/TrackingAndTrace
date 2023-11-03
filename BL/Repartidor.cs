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
                                     Nombre = repart.Nombre,
                                     ApellidoPaterno = repart.ApellidoPaterno,
                                     ApellidoMaterno = repart.ApellidoMaterno,
                                     IdUnidadAsignada = repart.IdUnidadAsignada,
                                     Telefono = repart.Telefono,
                                     FechaIngreso = repart.FechaIngreso,
                                     Fotografia = repart.Fotografia
                                 });
                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            ML.Repartidor repartidor = new ML.Repartidor();
                            repartidor.IdRepartidor = registro.IdRepartidor;
                            repartidor.Nombre = registro.Nombre;
                            repartidor.ApellidoPaterno = registro.ApellidoPaterno;
                            repartidor.ApellidoMaterno = registro.ApellidoMaterno;
                            repartidor.IdUnidadAsignada = registro.IdUnidadAsignada.Value;
                            repartidor.Telefono = registro.Telefono.Value;
                            repartidor.FechaIngreso = registro.FechaIngreso.Value;
                            repartidor.Fotografia = registro.Fotografia;
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
        public static bool Add(ML.Repartidor repartidor)
        {
            try
            {
                //todo lo que ejecute dentro de un using se libera al final
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    DL.Repartidor repartidorEntity = new DL.Repartidor();

                    repartidorEntity.Nombre = repartidor.Nombre;
                    repartidorEntity.ApellidoPaterno = repartidor.ApellidoPaterno;
                    repartidorEntity.ApellidoMaterno = repartidor.ApellidoMaterno;
                    repartidorEntity.IdUnidadAsignada = repartidor.IdUnidadAsignada;
                    repartidorEntity.Telefono = repartidor.Telefono;
                    repartidorEntity.FechaIngreso = repartidor.FechaIngreso;
                    repartidorEntity.Fotografia = repartidor.Fotografia;
                    context.Repartidor.Add(repartidorEntity);
                    int rowsAffected = context.SaveChanges();
                    if (rowsAffected > 0)
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
                                     Nombre = repart.Nombre,
                                     ApellidoPaterno = repart.ApellidoPaterno,
                                     ApellidoMaterno = repart.ApellidoMaterno,
                                     IdUnidadAsignada = repart.IdUnidadAsignada,
                                     Telefono = repart.Telefono,
                                     FechaIngreso = repart.FechaIngreso,
                                     Fotografia = repart.Fotografia
                                 });
                    if (query != null)
                    {
                        List<ML.Repartidor> list = new List<ML.Repartidor>();
                        foreach (var registro in query)
                        {
                            ML.Repartidor repartidorlist = new ML.Repartidor();
                            repartidorlist.IdRepartidor = registro.IdRepartidor;
                            repartidorlist.Nombre = registro.Nombre;
                            repartidorlist.ApellidoPaterno = registro.ApellidoPaterno;
                            repartidorlist.ApellidoMaterno = registro.ApellidoMaterno;
                            repartidorlist.IdUnidadAsignada = registro.IdUnidadAsignada.Value;
                            repartidorlist.Telefono = registro.Telefono.Value;
                            repartidorlist.FechaIngreso = registro.FechaIngreso.Value;
                            repartidorlist.Fotografia = registro.Fotografia;
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
                    var query = (from a in context.Repartidor
                                 where a.IdRepartidor == repartidor.IdRepartidor
                                 select a).SingleOrDefault();
                    if (query != null)
                    {
                        query.Nombre = repartidor.Nombre.ToString();
                        query.ApellidoPaterno = repartidor.ApellidoPaterno;
                        query.ApellidoMaterno = repartidor.ApellidoMaterno;
                        query.IdUnidadAsignada = repartidor.IdUnidadAsignada;
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
    }
}