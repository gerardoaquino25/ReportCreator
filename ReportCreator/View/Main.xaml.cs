using ReportCreator.Model;
using ReportCreator.View.Authentication;
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
    /// Lógica de interacción para Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        IRepository repo = new Repository();

        public Main()
        {
            InitializeComponent();
        }

        private void EnvioInformeClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new EnvioInforme();
        }

        private void OpcionesClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Opciones();
        }

        private void CerrarSesion(object sender, RoutedEventArgs e)
        {
            MainWindow.viewModel.Logout(new object());
            MainWindow.self.Content = new LoginWindow(MainWindow.viewModel);
        }
    }
}
