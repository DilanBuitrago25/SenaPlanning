using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseModelo
{
    public class Programs_Area_Red
    {
        public int IdPrograma { get; set; }
        public string DenominacionPrograma { get; set; }
        public string VersionPrograma { get; set; }
        public string NivelPrograma { get; set; }
        public Nullable<int> CodigoPrograma { get; set; }
        public string HorasPrograma { get; set; }
        public bool EstadoPrograma { get; set; }
        public string NombreArea { get; set; }
        public string NombreRed { get; set; }
    }
}
