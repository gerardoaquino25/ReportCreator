using ReportCreator.Entities;
using ReportCreator.Model;
using ReportCreator.View.Options;
using ReportCreator.View.UtilityElement;
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

            GuardarButtonUE guardarButton = new GuardarButtonUE();
            guardarButton.Name = "Guardar";
            guardarButton.Visibility = Visibility.Visible;
            guardarButton.MouseLeftButtonUp += Guardar_Click;
            MainWindow.AddButtonToInitBar(guardarButton);

            VolverButtonUE volverButton = new VolverButtonUE();
            volverButton.Name = "Volver";
            volverButton.Visibility = Visibility.Visible;
            volverButton.MouseLeftButtonUp += Volver_Click;
            MainWindow.AddButtonToInitBar(volverButton);
        }

        public DetalleMail(int mail_id)
            : this()
        {
            this.mail_id = mail_id;
            mailSender = repo.ObtenerMailSender(mail_id);
            Email.Text = mailSender.email;
            Password.Password = mailSender.password;
            Puerto.Text = mailSender.puerto.ToString();
            Smtp.Text = mailSender.smtp;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
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

            MainWindow.SetContent(new OpcionMailSenders());
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new OpcionMailSenders());
        }

        private void Probar_Click(object sender, RoutedEventArgs e)
        {
            Notificacion resultado = repo.EnviarEmail(Email.Text, Password.Password, Smtp.Text, Puerto.Text, EmailPrueba.Text, "Prueba", "Esto es una prueba.");
        }
    }
}
