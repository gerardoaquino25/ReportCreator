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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Main();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Internos();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Mail();
        }
    }
}
