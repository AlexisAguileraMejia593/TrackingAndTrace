using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Repartidor
    {
        public int IdRepartidor { get; set; }
        public ML.Unidad Unidad { get; set; }
        public int Telefono { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Fotografia { get; set; }
        public ML.Usuario Usuario { get; set; }
        public List<ML.Repartidor> Repartidores { get; set; }
    }
}