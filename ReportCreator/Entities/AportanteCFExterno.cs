using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.Entities
{
    public class AportanteCFExterno : AportanteCF
    {
        public Interno interno { get; set; }
    }
}
