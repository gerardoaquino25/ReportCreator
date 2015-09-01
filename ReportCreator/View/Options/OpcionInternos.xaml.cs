using ReportCreator.Entities;
using ReportCreator.Model;
using ReportCreator.View.UtilityElement;
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
    /// Lógica de interacción para Internos.xaml
    /// </summary>
    public partial class OpcionInternos : UserControl
    {
        IList<Interno> internos;
        IRepository repo = new Repository();

        public OpcionInternos()
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

            internos = repo.ObtenerInternos();
            InternosDG.ItemsSource = internos;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            repo.GuardarInternos((List<Interno>)InternosDG.ItemsSource);
            MainWindow.SetContent(new Opciones());
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new Opciones());
        }
    }
}
