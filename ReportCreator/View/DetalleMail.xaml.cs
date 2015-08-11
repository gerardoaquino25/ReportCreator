using ReportCreator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReportCreator.View
{
    /// <summary>
    /// Lógica de interacción para DetalleMail.xaml
    /// </summary>
    public partial class DetalleMail : UserControl
    {
        private int mail_id;
        IRepository repo = new Repository();
        MailSender mailSender;

        public DetalleMail()
        {
            InitializeComponent();
        }

        public DetalleMail(int mail_id)
        {
            InitializeComponent();
            this.mail_id = mail_id;
            mailSender = repo.ObtenerMailSender(mail_id);
            Email.Text = mailSender.email;
            Password.Password = mailSender.password;
            Puerto.Text = mailSender.puerto.ToString();
            Smtp.Text = mailSender.smtp;
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {
            if (mail_id == 0)
                repo.AgregarMailSender(Email.Text, Password.Password, Convert.ToInt32(Puerto.Text), Smtp.Text);
            else
                repo.GuardarMailSender(new MailSender()
                {
                    id = mail_id,
                    email = Email.Text,
                    password = Password.Password,
                    puerto = Convert.ToInt32(Puerto.Text),
                    smtp = Smtp.Text
                });

            MainWindow.self.Content = new MailSenders();
        }

        private void VolverClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new MailSenders();
        }

        private void ProbarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = Convert.ToInt32(Puerto.Text);
                client.Host = Smtp.Text;
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(Email.Text, Password.Password);

                MailMessage mm = new MailMessage(Email.Text, EmailPrueba.Text, "Prueba", "Esto es una prueba.");
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
            }
            catch (Exception exc)
            {

            }
        }
    }
}
