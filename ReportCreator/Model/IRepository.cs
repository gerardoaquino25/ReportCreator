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
        long AgregarCF(string cf);
        Externo AgregarExterno(string nombre, string observacion);
        Notificacion AgregarPadronCF(PadronCF padron);
        Notificacion AgregarAporteCF(AporteCF aporte);
        
        Informe ObtenerInforme(long id);
        IList<Informe> ObtenerInformesBorrador();
        IList<Interno> ObtenerInternos();
        EntradaCotizacion ObtenerEntradaCotizacion(long idEntrada);
        EntradaGenerica ObtenerEntradaGenerica(long idEntrada);
        IList<MailSender> ObtenerMailSenders();
        IList<MailReceiver> ObtenerMailReceivers();
        MailSender ObtenerMailSender(int id);
        IList<Externo> ObtenerAportantesCF();
        IList<CampaniaFinanciera> ObtenerCFs(string orderBy);
        IList<Externo> ObtenerExternos();
        IList<AporteCF> ObtenerAportesCF(long idEntrada);
        IList<PadronCF> ObtenerPadronesCF(long idEntrada);
        EntradaCampaniaFinanciera ObtenerEntradaCampaniaFinanciera(long idEntrada);
        
        Notificacion GuardarEntradaGenerica(EntradaGenerica entradaGenerica);
        Notificacion GuardarInforme(long idInforme, string asunto);
        Notificacion GuardarInternos(IList<Interno> internos);
        Notificacion GuardarEntradaCotizacion(EntradaCotizacion cotizacion);
        Notificacion GuardarMailSender(MailSender mailSender);
        Notificacion GuardarMailReceivers(IList<MailReceiver> receivers);

        Notificacion BorrarMailSender(int id);
        Notificacion BorrarEntrada(long id, int tipo);
        Notificacion BorrarInforme(long id);
    }
}