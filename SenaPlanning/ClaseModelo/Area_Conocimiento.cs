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
    
    public partial class Area_Conocimiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Area_Conocimiento()
        {
            this.Programa_Formacion = new HashSet<Programa_Formacion>();
            this.Instructor = new HashSet<Instructor>();
        }
    
        public int IdArea { get; set; }
        public int CodigoArea { get; set; }
        public string NombreArea { get; set; }
        public Nullable<int> IdRed { get; set; }
        public bool EstadoArea { get; set; }
    
        public virtual Red_Conocimiento Red_Conocimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Programa_Formacion> Programa_Formacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Instructor> Instructor { get; set; }
    }
}
