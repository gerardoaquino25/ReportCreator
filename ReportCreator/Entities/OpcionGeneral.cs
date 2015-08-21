using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.Entities
{
    public class OpcionGeneral
    {
        public const string ASUNTO_DEFAULT = "ASUNTO_DEFAULT";
        public const string CAMBIO_CONTRASENIA = "CAMBIO_CONTRASENIA";
        public const string VALUE = "VALUE";
        public const string CONTRASENIA_ACTUAL = "CONTRASENIA_ACTUAL";
        public const string CONTRASENIA_NUEVA = "CONTRASENIA_NUEVA";

        public bool cambio { get; set; }
        public string nombre { get; set; }
        public IDictionary<string, object> valores { get; set; }

        public OpcionGeneral()
        {
            this.cambio = false;
            valores = new Dictionary<string, object>();
        }
    }
}
