using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Paquetes
    {
        public static ML.Paquete Add(ML.Paquete paquete)
        {
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    DL.Paquete paqueteentity = new DL.Paquete();
                    paqueteentity.Detalle = paquete.Detalle;
                    paqueteentity.Peso = paquete.Peso;
                    paqueteentity.DireccionOrigen = paquete.DireccionOrigen;
                    paqueteentity.DireccionEntrega = paquete.DireccionEntrega;

                    context.Paquete.Add(paqueteentity);
                    int rowsAffected = context.SaveChanges();

                    if (rowsAffected > 0)
                    {
                        // Asigna el Id del repartidorEntity a repartidor
                        paquete.IdPaquete = paqueteentity.IdPaquete;
                        return paquete;
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
    }
}