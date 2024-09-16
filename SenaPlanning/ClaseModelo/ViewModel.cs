using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseModelo
{
    public class ResumenTrimestre
    {
        public int Trimestre { get; set; }
        public IEnumerable<ResumenAreaConocimiento> Datos { get; set; }
    }

    public class ResumenAreaConocimiento
    {
        public ClaseModelo.Ficha Ficha { get; set; }
        public string RedConocimiento { get; set; }
        public string AreaConocimiento { get; set; }
        public int NumeroFichas { get; set; }
        public int HorasRequeridas { get; set; }
        public int HorasContrato { get; set; }
        public int NumeroInstructoresPlanta { get; set; }
        public int NumeroInstructoresContrato { get; set; }


    }
}
