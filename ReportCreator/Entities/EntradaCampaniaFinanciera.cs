using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.Entities
{
    public class EntradaCampaniaFinanciera : Entrada
    {
        public CampaniaFinanciera campaniaFinanciera { get; set; }
        public IList<PadronCF> padrones { get; set; }
        public IList<AporteCF> aportes { get; set; }

        public EntradaCampaniaFinanciera()
        {
            this.padrones = new List<PadronCF>();
            this.aportes = new List<AporteCF>();
        }
    }
}
