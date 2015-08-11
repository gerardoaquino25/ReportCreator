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

namespace ReportCreator.View
{
    /// <summary>
    /// Lógica de interacción para Internos.xaml
    /// </summary>
    public partial class Internos : UserControl
    {
        IList<Interno> internos;
        IRepository repo = new Repository();

        public Internos()
        {
            InitializeComponent();
            internos = repo.ObtenerInternos();
            InternosDG.ItemsSource = internos;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            repo.GuardarInternos((List<Interno>)InternosDG.ItemsSource);
            MainWindow.self.Content = new Opciones();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Opciones();
        }
    }
}
