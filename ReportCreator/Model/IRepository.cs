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
        long AgregarMailSender(string email, string password, int puerto, string smtp);
        
        Informe ObtenerInforme(long id);
        IList<Informe> ObtenerInformesBorrador();
        IList<Interno> ObtenerInternos();
        EntradaCotizacion ObtenerEntradaCotizacion(long idEntrada);
        EntradaGenerica ObtenerEntradaGenerica(long idEntrada);
        IList<MailSender> ObtenerMailSenders();
        MailSender ObtenerMailSender(int id);
        
        Notificacion GuardarEntradaGenerica(EntradaGenerica entradaGenerica);
        Notificacion GuardarInforme(long idInforme, string asunto);
        Notificacion GuardarInternos(IList<Interno> internos);
        Notificacion GuardarEntradaCotizacion(EntradaCotizacion cotizacion);
        Notificacion GuardarMailSender(int id, string email, string password, int puerto, string smtp);
        
        Notificacion BorrarMailSender(int id);
    }
}