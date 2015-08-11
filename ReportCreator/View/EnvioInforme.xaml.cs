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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new PendienteEnvio();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Borrador();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Main();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Borradores();
        }
    }
}
