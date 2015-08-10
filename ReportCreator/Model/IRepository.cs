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
        long AgregarEntrada(long idInforme, string asunto, int tipo);
        long AgregarMailSender(string email, string password, int puerto, string smtp);

        Notificacion GuardarEntradaGenerica(long idEntrada, string data);
        Informe GetInforme(long id);
        Notificacion GuardarInforme(long idInforme, string asunto);
        IList<Informe> GetInformesBorrador();
        IList<Interno> GetInternos();
        Notificacion GuardarInternos(IList<Interno> internos);
        Notificacion GuardarMailSender(int id, string email, string password, int puerto, string smtp);

        IList<MailSender> ObtenerMailSenders();
        MailSender ObtenerMailSender(int id);

        Notificacion BorrarMailSender(int id);
    }
}