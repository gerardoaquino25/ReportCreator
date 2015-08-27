using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class Tipo
    {
        public int id;
        public string descripcion { get; set; }

        public Tipo(int id, string descripcion)
        {
            this.id = id;
            this.descripcion = descripcion;
        }

        public override bool Equals(Object tipo)
        {
            if (tipo == null)
                return false;

            Tipo p = tipo as Tipo;
            if ((System.Object)p == null)
                return false;

            return id == p.id;
        }
    }
}
