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
    
    public partial class Remitente
    {
        public Remitente()
        {
            this.RemitenteInforme = new HashSet<RemitenteInforme>();
            this.HistorialEnvioRemitente = new HashSet<HistorialEnvioRemitente>();
        }
    
        public int ID { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
    
        public virtual ICollection<RemitenteInforme> RemitenteInforme { get; set; }
        public virtual ICollection<HistorialEnvioRemitente> HistorialEnvioRemitente { get; set; }
    }
}