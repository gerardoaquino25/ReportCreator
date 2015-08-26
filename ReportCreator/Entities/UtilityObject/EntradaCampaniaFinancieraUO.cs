using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.Entities.UtilityObject
{
    public class EntradaCampaniaFinancieraUO : Entrada
    {
        public CampaniaFinanciera campaniaFinanciera { get; set; }
        public IList<PadronCF> padrones { get; set; }
        public IList<AporteCF> aportes { get; set; }

        public EntradaCampaniaFinancieraUO()
        {
            this.padrones = new List<PadronCF>();
            this.aportes = new List<AporteCF>();
        }
    }
}
