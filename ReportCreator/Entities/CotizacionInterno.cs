using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class CotizacionInterno
    {
        public long id { get; set; }
        public Interno interno { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public int pago { get; set; }
        public string observacion { get; set; }
    }
}
