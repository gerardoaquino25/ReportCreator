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
    /// Lógica de interacción para EntradaCampaniaFinanciera.xaml
    /// </summary>
    public partial class EntradaCampaniaFinanciera : UserControl
    {
        private long idInforme;
        private string asunto;
        private bool nuevo;
        private long idEntrada;
        IRepository repo = new Repository();

        public EntradaCampaniaFinanciera(long idEntrada)
        {
            InitializeComponent();
            //CampaniaAsociadaLabel.Visibility = System.Windows.Visibility.Collapsed;
            //CampaniaAsociada.Visibility = System.Windows.Visibility.Collapsed;
            //InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            //Interno.Visibility = System.Windows.Visibility.Collapsed;
            //ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            //Externo.Visibility = System.Windows.Visibility.Collapsed;
            //NombreLabel.Visibility = System.Windows.Visibility.Collapsed;
            //Nombre.Visibility = System.Windows.Visibility.Collapsed;
            this.idEntrada = idEntrada;
            this.nuevo = false;
        }

        public EntradaCampaniaFinanciera(long idInforme, string asunto)
        {
            InitializeComponent();
            CampaniaAsociadaLabel.Visibility = System.Windows.Visibility.Collapsed;
            CampaniaAsociada.Visibility = System.Windows.Visibility.Collapsed;
            //InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            //Interno.Visibility = System.Windows.Visibility.Collapsed;
            ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            Externo.Visibility = System.Windows.Visibility.Collapsed;
            //PagoLabel.Visibility = System.Windows.Visibility.Collapsed;
            //Pago.Visibility = System.Windows.Visibility.Collapsed;
            NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            NombreExterno.Visibility = System.Windows.Visibility.Collapsed;
            //FechaAporteLabel.Visibility = System.Windows.Visibility.Collapsed;
            //FechaAporte.Visibility = System.Windows.Visibility.Collapsed;
            TipoAporte.SelectedIndex = 0;
            TipoAportante.SelectedIndex = 0;
            this.idInforme = idInforme;
            this.asunto = asunto;
            this.nuevo = true;
            iniciar(nuevo);
        }

        private void iniciar(bool nuevo)
        {
            Interno.ItemsSource = repo.ObtenerInternos();
            CampaniaAsociada.ItemsSource = repo.ObtenerCFExistentes();
            Externo.ItemsSource = repo.ObtenerAportantesCF();
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {
            switch (TipoAporte.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }

        private void VolverClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
