using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.Entities
{
    public class Cotizacion
    {
        public int id { get; set; }
        public int mes { get; set; }
        public int anio { get; set; }
        public IList<CotizacionInterno> cotizacionesInternos { get; set; }
    }
}
