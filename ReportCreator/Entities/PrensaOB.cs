using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class PrensaOB
    {
        public long? id { get; set; }
        public long? entradaPrensaId { get; set; }
        public Tipo tipoPasaje { get; set; }
        public Interno interno { get; set; }
        public int prensaNumero { get; set; }
        public int aporte { get; set; }
        public object comprador { get; set; }
        public long? actividadId { get; set; }
        public string observacion { get; set; }
        public bool modificado { get; set; }

        public PrensaOB()
        {
            modificado = false;
        }

        public override bool Equals(Object prensaUO)
        {
            if (prensaUO == null)
                return false;

            PrensaOB p = prensaUO as PrensaOB;
            if ((System.Object)p == null)
                return false;

            return id == p.id;
        }
    }
}
