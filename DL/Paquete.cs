//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Paquete
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paquete()
        {
            this.Entrega = new HashSet<Entrega>();
        }
    
        public int IdPaquete { get; set; }
        public string Detalle { get; set; }
        public string Peso { get; set; }
        public string DireccionOrigen { get; set; }
        public string DireccionEntrega { get; set; }
        public Nullable<System.DateTime> FechaEstimadaEntrega { get; set; }
        public string CodigoRastreo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entrega> Entrega { get; set; }
    }
}
