using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Unidad
    {
        public int IdUnidad { get; set; }
        public string NumeroPlaca { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string AñoFabricacion { get; set; }
        public ML.EstatusUnidad EstatusUnidad { get; set; }
        public List<Unidad> Unidades { get; set; }
    }
}