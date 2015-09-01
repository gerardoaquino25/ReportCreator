using ReportCreator.View.Options;
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
    /// Lógica de interacción para Opciones.xaml
    /// </summary>
    public partial class Opciones : UserControl
    {
        public Opciones()
        {
            InitializeComponent();
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new Main());
        }

        private void Internos_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new OpcionInternos());
        }

        private void MailSenders_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new OpcionMailSenders());
        }

        private void MailReceivers_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new OpcionMailReceivers());
        }

        private void General_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new OpcionGeneralUC());
        }
    }
}
