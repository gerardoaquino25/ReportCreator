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
    }
}
