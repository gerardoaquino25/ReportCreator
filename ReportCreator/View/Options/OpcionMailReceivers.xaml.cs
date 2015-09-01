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
    /// Lógica de interacción para MailReceivers.xaml
    /// </summary>
    public partial class OpcionMailReceivers : UserControl
    {
        IList<MailReceiver> mailReceivers;
        IRepository repo = new Repository();

        public OpcionMailReceivers()
        {
            InitializeComponent();
            mailReceivers = repo.ObtenerMailReceivers();
            MailReceiversDG.ItemsSource = mailReceivers;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            repo.GuardarMailReceivers((List<MailReceiver>)MailReceiversDG.ItemsSource);
            MainWindow.SetContent(new Opciones());
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new Opciones());
        }
    }
}
