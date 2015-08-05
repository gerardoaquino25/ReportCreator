using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class Entrada
    {
        public long id { get; set; }
        public long idInforme { get; set; }
        public TipoEntrada tipo { get; set; }
        public string asunto { get; set; }
        public string tipoDescripcion { get; set; }
    }
}
