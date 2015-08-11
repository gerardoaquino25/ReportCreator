using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class Interno : object
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

        public override bool Equals(Object interno)
        {
            // If parameter is null return false.
            if (interno == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Interno p = interno as Interno;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return id == p.id;
        }

        public override Int32 GetHashCode()
        {
            if (id == 0)
                return 0;

            return id.GetHashCode() * nombre.GetHashCode() * circulo.GetHashCode() * activo.GetHashCode();
        }
    }
}
