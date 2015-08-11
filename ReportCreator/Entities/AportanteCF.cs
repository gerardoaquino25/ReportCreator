using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.Entities
{
    public class AportanteCF
    {
        public long id { get; set; }
        public long aportante_id { get; set; }
        public int compromiso { get; set; }
        public int pago { get; set; }
        public DateTime fechaAporte { get; set; }
        public long campaniaFinancieraId { get; set; }
        public string observacion { get; set; }
    }
}
