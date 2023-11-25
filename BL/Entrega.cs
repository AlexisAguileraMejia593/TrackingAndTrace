using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Entrega
    {
        public static ML.Entrega GetAll(string nombreRepartidor, string apellidoPaternoRepartidor)
        {
            ML.Entrega entregaobject = new ML.Entrega();
            entregaobject.Entregas = new List<ML.Entrega>();
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var query = from entrega in context.Entrega
                                 join paquete in context.Paquete on entrega.IdPaquete equals paquete.IdPaquete
                                 join repartidor in context.Repartidor on entrega.IdRepartidor equals repartidor.IdRepartidor
                                 join usuario in context.Usuario on repartidor.IdUsuario equals usuario.IdUsuario
                                 join rol in context.Rol on usuario.IdRol equals rol.IdRol
                                 join estatusentrega in context.EstatusEntrega on entrega.IdEstatusEntrega equals estatusentrega.IdEstatus
                                where usuario.Nombre.Contains(nombreRepartidor) && usuario.ApellidoPaterno.Contains(apellidoPaternoRepartidor)
                                select new
                                 {
                                     entrega.IdEntrega,
                                     entrega.FechaEntrega,
                                     paquete.IdPaquete,
                                     paquete.Detalle,
                                     paquete.Peso,
                                     paquete.DireccionOrigen,
                                     paquete.DireccionEntrega,
                                     paquete.FechaEstimadaEntrega,
                                     paquete.CodigoRastreo,
                                     repartidor.IdRepartidor,
                                     repartidor.IdUnidadAsignada,
                                     repartidor.Telefono,
                                     repartidor.FechaIngreso,
                                     repartidor.Fotografia,
                                     estatusentrega.IdEstatus,
                                     estatusentrega.Estatus,
                                     usuario.IdUsuario,
                                     usuario.UserName,
                                     usuario.Email,
                                     usuario.Nombre,
                                     usuario.ApellidoPaterno,
                                     usuario.ApellidoMaterno,
                                     usuario.Password,
                                     rol.IdRol,
                                     rol.Tipo
                                 };
                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            ML.Entrega entrega = new ML.Entrega();
                            entrega.IdEntrega = registro.IdEntrega;
                            entrega.FechaEntrega = registro.FechaEntrega.Value;
                            entrega.Paquete = new ML.Paquete();
                            entrega.Paquete.Detalle = registro.Detalle;
                            entrega.Paquete.Peso = registro.Peso;
                            entrega.Paquete.DireccionOrigen = registro.DireccionOrigen;
                            entrega.Paquete.DireccionEntrega = registro.DireccionEntrega;
                            entrega.Paquete.FechaEstimadaEntrega = registro.FechaEstimadaEntrega.Value;
                            entrega.Paquete.CodigoRastreo = registro.CodigoRastreo;
                            entrega.Repartidor = new ML.Repartidor();
                            entrega.EstatusEntrega = new ML.EstatusEntrega();
                            entrega.EstatusEntrega.Estatus = registro.Estatus;
                            entrega.Repartidor.Usuario = new ML.Usuario();
                            entrega.Repartidor.Usuario.Nombre = registro.Nombre;
                            entrega.Repartidor.Usuario.Rol = new ML.Rol();
                            entrega.Repartidor.Usuario.Rol.IdRol = registro.IdRol;
                            entrega.Repartidor.Usuario.Rol.Tipo = registro.Tipo;
                            entregaobject.Entregas.Add(entrega);
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
            return entregaobject;
        }
        public static ML.Entrega GetByCodigo(string codigoRastreo)
        {
            ML.Entrega result = null;
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var query = from entrega in context.Entrega
                                join paquete in context.Paquete on entrega.IdPaquete equals paquete.IdPaquete
                                join repartidor in context.Repartidor on entrega.IdRepartidor equals repartidor.IdRepartidor
                                join usuario in context.Usuario on repartidor.IdUsuario equals usuario.IdUsuario
                                join estatusentrega in context.EstatusEntrega on entrega.IdEstatusEntrega equals estatusentrega.IdEstatus
                                where paquete.CodigoRastreo == codigoRastreo
                                select new
                                {
                                    entrega.IdEntrega,
                                    entrega.FechaEntrega,
                                    paquete.IdPaquete,
                                    paquete.Detalle,
                                    paquete.Peso,
                                    paquete.DireccionOrigen,
                                    paquete.DireccionEntrega,
                                    paquete.FechaEstimadaEntrega,
                                    paquete.CodigoRastreo,
                                    repartidor.IdRepartidor,
                                    repartidor.IdUnidadAsignada,
                                    repartidor.Telefono,
                                    repartidor.FechaIngreso,
                                    repartidor.Fotografia,
                                    estatusentrega.IdEstatus,
                                    estatusentrega.Estatus,
                                    usuario.IdUsuario,
                                    usuario.Nombre,
                                    usuario.ApellidoPaterno,
                                    usuario.ApellidoMaterno
                                };
                    if (query != null)
                    {
                        List<ML.Entrega> list = new List<ML.Entrega>();
                        foreach (var registro in query)
                        {
                            ML.Entrega entregalist = new ML.Entrega();
                            entregalist.IdEntrega = registro.IdEntrega;
                            entregalist.FechaEntrega = registro.FechaEntrega.Value;
                            entregalist.Paquete = new ML.Paquete();
                            entregalist.Paquete.DireccionOrigen = registro.DireccionOrigen;
                            entregalist.Paquete.DireccionEntrega = registro.DireccionEntrega;
                            entregalist.Paquete.CodigoRastreo = registro.CodigoRastreo;
                            entregalist.Repartidor = new ML.Repartidor();
                            entregalist.EstatusEntrega = new ML.EstatusEntrega();
                            entregalist.EstatusEntrega.Estatus = registro.Estatus;
                            entregalist.Repartidor.Usuario = new ML.Usuario();
                            entregalist.Repartidor.Usuario.Nombre = registro.Nombre;
                            // Boxing
                            object boxedRepartidor = entregalist;
                            list.Add(entregalist);
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
