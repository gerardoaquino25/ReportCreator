using ReportCreator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.Model
{
    public interface IRepository
    {
        long AgregarInforme(string titulo);
        long AgregarEntrada(long idInforme, string titulo, int tipo);
        Notificacion GuardarEntradaGenerica(EntradaGenerica entradaGenerica);
        Informe ObtenerInforme(long id);
        Notificacion GuardarInforme(long idInforme, string asunto);
        IList<Informe> ObtenerInformesBorrador();
        IList<Interno> ObtenerInternos();
        Notificacion GuardarInternos(IList<Interno> internos);
        Notificacion GuardarEntradaCotizacion(EntradaCotizacion cotizacion);
        EntradaCotizacion ObtenerEntradaCotizacion(long idEntrada);
        EntradaGenerica ObtenerEntradaGenerica(long idEntrada);
    }
}