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
    
    public partial class RemitenteInforme
    {
        public int Id { get; set; }
        public string RemitenteID { get; set; }
    
        public virtual Remitente Remitente { get; set; }
        public virtual Informe Informe { get; set; }
    }
}
