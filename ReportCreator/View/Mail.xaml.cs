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

namespace ReportCreator.View
{
    /// <summary>
    /// Lógica de interacción para Mail.xaml
    /// </summary>
    public partial class Mail : UserControl
    {
        IRepository repo = new Repository();

        public Mail()
        {
            InitializeComponent();
            MailSenders.ItemsSource = repo.ObtenerMailSenders();
        }

        private void AgregarClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new DetalleMail();
        }

        private void VolverClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Opciones();
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
            MainWindow.self.Content = new DetalleMail(((MailSender)row.Item).id);
        }
    }
}
