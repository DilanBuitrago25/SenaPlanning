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
    
    public partial class Programa_Formacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Programa_Formacion()
        {
            this.Ficha = new HashSet<Ficha>();
        }
    
        public int IdPrograma { get; set; }
        public string DenominacionPrograma { get; set; }
        public string VersionPrograma { get; set; }
        public string NivelPrograma { get; set; }
        public Nullable<int> IdArea { get; set; }
        public bool EstadoPrograma { get; set; }
    
        public virtual Area_Conocimiento Area_Conocimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ficha> Ficha { get; set; }
    }
}
