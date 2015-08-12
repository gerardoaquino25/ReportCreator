using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class CampaniaFinanciera
    {
        public long id { get; set; }
        public string nombre { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string nombreAMostar
        {
            get
            {
                return nombre + " - " + fechaCreacion.Day + "/" + fechaCreacion.Month + "/" + fechaCreacion.Year;
            }
        }
    }
}
