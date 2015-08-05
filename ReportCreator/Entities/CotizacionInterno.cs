using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class CotizacionInterno
    {
        public Interno interno { get; set; }
        public DateTime fecha { get; set; }
        public string observacion { get; set; }
    }
}
