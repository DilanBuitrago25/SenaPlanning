using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public string CodigoPrograma { get; set; }
        public string HorasPrograma { get; set; }
        public bool EstadoPrograma { get; set; } = true;
        public string NombreArea { get; set; }
        public string NombreRed { get; set; }
    }

    public class MetaAndOferta
    {
        public List<Meta> Metas { get; set; }
        public  List<Oferta> Ofertas { get; set; }
    }

}
