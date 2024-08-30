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
        public bool EstadoOferta { get; set; }
        public string NombreOferta { get; set; }
        public string FechaInicioOferta { get; set; }
        public string MetaOferta { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdMetas { get; set; }
        public Nullable<int> IdRed { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ficha> Ficha { get; set; }
        public virtual Red_Conocimiento Red_Conocimiento { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Meta Meta { get; set; }
    }
}
