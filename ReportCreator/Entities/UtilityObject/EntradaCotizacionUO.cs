using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.Entities.UtilityObject
{
    public class EntradaCotizacionUO : Entrada
    {
        public int mes { get; set; }
        public int anio { get; set; }
        public IList<CotizacionInterno> cotizacionesInternos { get; set; }
    }
}
