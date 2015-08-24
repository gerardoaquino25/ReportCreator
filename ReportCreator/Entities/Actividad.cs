using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class Actividad
    {
        public long id { get; set; }
        public string nombre { get; set; }
        public string detalle { get; set; }
        public DateTime fecha { get; set; }
    }
}
