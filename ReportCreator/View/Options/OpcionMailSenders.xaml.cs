using ReportCreator.Entities;
using ReportCreator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ReportCreator.View.Options
{
    /// <summary>
    /// Lógica de interacción para Mail.xaml
    /// </summary>
    public partial class OpcionMailSenders : UserControl
    {
        IRepository repo = new Repository();

        public OpcionMailSenders()
        {
            InitializeComponent();
            MailSendersDG.ItemsSource = repo.ObtenerMailSenders();
        }

        private void AgregarClick(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new DetalleMail(), true);
        }

        private void VolverClick(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new Opciones(), true);
        }

        private void Row_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Delete == e.Key)
            {
                DataGridRow row = sender as DataGridRow;
                repo.BorrarMailSender(((MailSender)row.Item).id);
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            MainWindow.SetContent(new DetalleMail(((MailSender)row.Item).id), true);
        }
    }
}
