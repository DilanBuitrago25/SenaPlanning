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
    
    public partial class Instructor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Instructor()
        {
            this.Area_Conocimiento = new HashSet<Area_Conocimiento>();
        }
    
        public int IdInstructor { get; set; }
        public Nullable<int> DocumentoInstructor { get; set; }
        public string NombreCompletoInstructor { get; set; }
        public Nullable<int> CodRegionalInstructor { get; set; }
        public string RegionalInstructor { get; set; }
        public Nullable<int> CodCCOS { get; set; }
        public string DependenciaInstructor { get; set; }
        public string CargoInstructor { get; set; }
        public string TipoCargoInstructor { get; set; }
        public string CorreoSENAInstructor { get; set; }
        public string RedInstructor { get; set; }
        public string AreaInstructor { get; set; }
        public string RutaInstructor { get; set; }
        public Nullable<int> CodMunicipioInstructor { get; set; }
        public string MunicipioInstructor { get; set; }
        public string FechaNacimientoInstructor { get; set; }
        public string FechaIngreso { get; set; }
        public string SexoInstructor { get; set; }
        public bool EstadoInstructor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Area_Conocimiento> Area_Conocimiento { get; set; }
    }
}
