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
    
    public partial class Meta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meta()
        {
            this.Oferta = new HashSet<Oferta>();
        }
    
        public int IdMeta { get; set; }
        public string MetaFecha { get; set; }
        public Nullable<int> MetaTecnPresencial { get; set; }
        public Nullable<int> MetaTecnVirtual { get; set; }
        public Nullable<int> MetaTecPresencial { get; set; }
        public Nullable<int> MetaTecVirtual { get; set; }
        public Nullable<int> MetaETPresencial { get; set; }
        public Nullable<int> MetaETVirtual { get; set; }
        public Nullable<int> MetaOtros { get; set; }
        public bool EstadoMeta { get; set; }
        public Nullable<int> MetaTGOApPasan { get; set; }
        public Nullable<int> MetaTCOApPasan { get; set; }
        public Nullable<int> MetaETApPasan { get; set; }
        public Nullable<int> MetaOTROApPasan { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
