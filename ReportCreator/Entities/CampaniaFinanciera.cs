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

        public override bool Equals(Object campaniaFinanciera)
        {
            // If parameter is null return false.
            if (campaniaFinanciera == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            CampaniaFinanciera p = campaniaFinanciera as CampaniaFinanciera;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return id == p.id;
        }
    }
}
