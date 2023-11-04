using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServiceRepartidor" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServiceRepartidor.svc o ServiceRepartidor.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServiceRepartidor : IServiceRepartidor
    {
        public Repartidor Add(ML.Repartidor repartidor)
        {
            Repartidor newRepartidor = BL.Repartidor.Add(repartidor);
            return newRepartidor;
        }
        public bool Update(ML.Repartidor repartidor)
        {
            return BL.Repartidor.Update(repartidor);
        }

        public bool Delete(int IdRepartidor)
        {
            return BL.Repartidor.Delete(IdRepartidor);
        }

        Repartidor IServiceRepartidor.Update(Repartidor repartidor)
        {
            throw new NotImplementedException();
        }

        Repartidor IServiceRepartidor.Delete(int? IdRepartidor)
        {
            throw new NotImplementedException();
        }

        Repartidor IServiceRepartidor.GetAll(Repartidor repartidor)
        {
            Repartidor newRepartidor = BL.Repartidor.GetAll();
            return newRepartidor;
        }

        public Repartidor GetById(int IdRepartidor)
        {
            throw new NotImplementedException();
        }
    }
}
