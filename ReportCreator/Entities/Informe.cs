using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.Entities
{
    public class Informe
    {
        public long id { get; set; }
        public string asunto { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int estado { get; set; }
        public IList<Entrada> entradas { get; set; }
    }
}
