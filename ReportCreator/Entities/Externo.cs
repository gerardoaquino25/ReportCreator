using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.Entities
{
    public class Externo
    {
        public long id { get; set; }
        public string nombre { get; set; }
        public string observacion { get; set; }
        public Interno internoAsociado { get; set; }
        public string type
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public override bool Equals(Object externo)
        {
            // If parameter is null return false.
            if (externo == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Externo p = externo as Externo;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return id == p.id;
        }
    }
}
