//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReportCreator.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class HistorialEnvioRemitente
    {
        public HistorialEnvioRemitente()
        {
            this.ReporteEnvio = new HashSet<ReporteEnvio>();
        }
    
        public long ID { get; set; }
        public long ReporteEnvioID { get; set; }
        public int RemitenteID { get; set; }
    
        public virtual Remitente Remitente { get; set; }
        public virtual ICollection<ReporteEnvio> ReporteEnvio { get; set; }
    }
}