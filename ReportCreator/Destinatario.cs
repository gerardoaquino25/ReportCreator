//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReportCreator
{
    using System;
    using System.Collections.Generic;
    
    public partial class Destinatario
    {
        public Destinatario()
        {
            this.DestinatarioInforme = new HashSet<DestinatarioInforme>();
            this.HistorialEnvioDestinatario = new HashSet<HistorialEnvioDestinatario>();
        }
    
        public int ID { get; set; }
        public string Email { get; set; }
    
        public virtual ICollection<DestinatarioInforme> DestinatarioInforme { get; set; }
        public virtual ICollection<HistorialEnvioDestinatario> HistorialEnvioDestinatario { get; set; }
    }
}
