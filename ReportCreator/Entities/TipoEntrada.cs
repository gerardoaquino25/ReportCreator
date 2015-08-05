using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class TipoEntrada
    {
        public int id;
        public string descripcion;

        public TipoEntrada(int id, string descripcion)
        {
            this.id = id;
            this.descripcion = descripcion;
        }
    }
}
