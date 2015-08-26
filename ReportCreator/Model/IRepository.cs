using ReportCreator.Entities;
using ReportCreator.Entities.UtilityObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Externo AgregarExterno(string nombre, string observacion, Interno interno);
        Notificacion AgregarPadronCF(PadronCF padron);
        Notificacion AgregarAporteCF(AporteCF aporte);
        Notificacion AgregarUsuario(string username, string email, string password);
        long AgregarActividad(Actividad actividad);
        Notificacion AgregarEntradaPrensa(EntradaPrensaUO prensaUO);

        Informe ObtenerInforme(long id);
        IList<Informe> ObtenerInformesBorrador();
        IList<Interno> ObtenerInternos();
        EntradaCotizacionUO ObtenerEntradaCotizacion(long idEntrada);
        EntradaGenericaUO ObtenerEntradaGenerica(long idEntrada);
        IList<MailSender> ObtenerMailSenders();
        IList<MailReceiver> ObtenerMailReceivers();
        MailSender ObtenerMailSender(int id);
        IList<CampaniaFinanciera> ObtenerCFs(string orderBy);
        IList<Externo> ObtenerExternos();
        IList<AporteCF> ObtenerAportesCF(long entradaId, long campaniaFinancieraId);
        IList<PadronCF> ObtenerPadronesCF(long entradaId, long campaniaFinancieraId);
        EntradaCampaniaFinancieraUO ObtenerEntradaCampaniaFinanciera(long idEntrada);
        PrensaUO ObtenerPrensa(long id, bool closeConnection = true);
        Suscripcion ObtenerSuscripcion(long id, bool closeConnection = true);
        ObservableCollection<Suscripcion> ObtenerSuscripciones(bool closeConnection = true);
        EntradaPrensaUO ObtenerEntradaPrensa(long idEntrada);
        IList<Tipo> ObtenerPrensaTipoPasaje();
        IList<Actividad> ObtenerActividades();
        ObservableCollection<PrensaUO> ObtenerPrensasByEntradaId(long entradaId, bool closeConnection = true);

        Notificacion GuardarEntradaGenerica(EntradaGenericaUO entradaGenerica);
        Notificacion GuardarInforme(long idInforme, string asunto);
        Notificacion GuardarInternos(IList<Interno> internos);
        Notificacion GuardarEntradaCotizacion(EntradaCotizacionUO cotizacion);
        Notificacion GuardarMailSender(MailSender mailSender);
        Notificacion GuardarMailReceivers(IList<MailReceiver> receivers);
        Notificacion GuardarAporteCF(AporteCF aporte);
        Notificacion GuardarPadronCF(PadronCF padron);
        Notificacion GuardarEntradaCampaniaFinanciera(EntradaCampaniaFinancieraUO entradaCampaniaFinanciera);
        Notificacion GuardarOpcionesGenerales(IList<OpcionGeneral> listaOpcionesGenerales);
        Notificacion GuardarPrensa(PrensaUO prensa);
        Notificacion GuardarSuscripcion(Suscripcion suscripcion);

        Notificacion BorrarMailSender(int id);
        Notificacion BorrarEntrada(long id, int tipo);
        Notificacion BorrarInforme(long id);
        Notificacion BorrarPrensa(long id);
        Notificacion BorrarSuscripcion(long id);

        Notificacion RecuperarContraseña(string username);
        Notificacion EnviarEmail(string emailFrom, string password, string smtp, string puerto, string emailTo, string subject, string html);
    }
}