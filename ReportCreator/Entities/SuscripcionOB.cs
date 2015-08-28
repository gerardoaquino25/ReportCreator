using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.Entities
{
    public class SuscripcionOB
    {
        public long? id { get; set; }
        public int tipoSuscripcion { get; set; }
        public object suscriptor { get; set; }
        public DateTime fechaSuscripcion { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string observacion { get; set; }
        public int usuarioId { get; set; }
        public long entradaPrensaId { get; set; }
        public bool modificado { get; set; }
    }
}
