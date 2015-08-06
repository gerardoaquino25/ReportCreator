using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class Interno
    {
        public long id { get; set; }
        public string nombre { get; set; }
        public string circulo { get; set; }
        public bool activo { get; set; }
        public string nombreCompleto
        {
            get
            {
                return String.IsNullOrEmpty(circulo) ? nombre : nombre + "-" + circulo;
            }
        }
    }
}
