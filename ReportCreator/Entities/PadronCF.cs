using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.Entities
{
    public class PadronCF
    {
        public long id { get; set; }
        public long aportanteId
        {
            get
            {
                switch (this.tipoAportante)
                {
                    case 1:
                        return ((Interno)this.aportante).id;
                    case 2:
                        return ((Externo)this.aportante).id;
                }
                return 0;
            }
        }
        public int compromiso { get; set; }
        public long campaniaFinancieraId { get; set; }
        public string observacion { get; set; }
        public long entradaCampaniaFinancieraId { get; set; }
        public object aportante { get; set; }
        public int tipoAportante
        {
            get
            {
                if (this.aportante.GetType() == typeof(Interno))
                    return 1;
                if (this.aportante.GetType() == typeof(Externo))
                    return 2;
                return 0;
            }
        }
        public bool perteneceEntrada { get; set; }
        public override bool Equals(Object campaniaFinanciera)
        {
            // If parameter is null return false.
            if (campaniaFinanciera == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            PadronCF p = campaniaFinanciera as PadronCF;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return id == p.id;
        }
    }
}
