using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServiceRepartidor" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceRepartidor
    {
        [OperationContract]
        ML.Repartidor Add(ML.Repartidor repartidor);
        [OperationContract]
        ML.Repartidor Update(ML.Repartidor repartidor);
        [OperationContract]
        ML.Repartidor Delete(int? IdRepartidor);
        [OperationContract]
        [ServiceKnownType(typeof(ML.Repartidor))]
        ML.Repartidor GetAll(ML.Repartidor repartidor);
        [OperationContract]
        Repartidor GetById(int IdRepartidor);
    }
}
