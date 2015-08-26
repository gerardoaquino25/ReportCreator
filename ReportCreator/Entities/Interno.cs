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

        public string type
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public override bool Equals(Object interno)
        {
            if (interno == null)
                return false;

            Interno p = interno as Interno;
            if ((System.Object)p == null)
                return false;

            return id == p.id;
        }
    }
}
