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
    /// Lógica de interacción para EnvioInforme.xaml
    /// </summary>
    public partial class EnvioInforme : UserControl
    {
        public EnvioInforme()
        {
            InitializeComponent();
        }

        private void PendienteEnvio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new PendienteEnvio(), true);
        }

        private void Borrador_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new Borrador(), true);
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new Main(), true);
        }

        private void Borradores_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new Borradores(), true);
        }
    }
}
