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

namespace ReportCreator.View
{
    /// <summary>
    /// Lógica de interacción para AgregarCF.xaml
    /// </summary>
    public partial class AgregarCF : UserControl
    {
        IRepository repo = new Repository();
        private bool nuevo;
        private long idEntrada;

        public AgregarCF(long idEntrada, bool nuevo)
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

            this.nuevo = nuevo;
            this.idEntrada = idEntrada;
        }

        private bool ValidarDatos()
        {
            return true;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {

            if (ValidarDatos())
            {
                repo.AgregarCF(NombreCampania.Text);
                MainWindow.SetContent(new EntradaCampaniaFinanciera(idEntrada, nuevo, this.nuevo));
            }
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new EntradaCampaniaFinanciera(idEntrada, nuevo, this.nuevo));
        }
    }
}
