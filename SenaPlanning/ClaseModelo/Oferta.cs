//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClaseModelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Oferta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oferta()
        {
            this.Ficha = new HashSet<Ficha>();
        }
    
        public int IdOferta { get; set; }
        public Nullable<int> CodigoOferta { get; set; }
        public Nullable<int> HorasContTrimIOferta { get; set; }
        public Nullable<int> HorasContTrimIIOferta { get; set; }
        public Nullable<int> HorasContTrimIIIOferta { get; set; }
        public Nullable<int> HorasContTrimIVOferta { get; set; }
        public Nullable<int> CantidadInstContratoTrimIOferta { get; set; }
        public Nullable<int> CantidadInstContratoTrimIIOferta { get; set; }
        public Nullable<int> CantidadInstContratoTrimIIIOferta { get; set; }
        public Nullable<int> CantidadInstContratoTrimIVOferta { get; set; }
        public bool EstadoOferta { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdMetas { get; set; }
        public Nullable<int> IdArea { get; set; }
    
        public virtual Area_Conocimiento Area_Conocimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ficha> Ficha { get; set; }
        public virtual Meta Meta { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
