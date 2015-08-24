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
    /// Lógica de interacción para AgregarPrensa.xaml
    /// </summary>
    public partial class AgregarPrensa : UserControl
    {
        public AgregarPrensa()
        {
            InitializeComponent();
        }

        private void ExternoExistenteCheck(object sender, RoutedEventArgs e)
        {
            NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

            ExternoLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void ExternoExistenteUncheck(object sender, RoutedEventArgs e)
        {
            ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

            NombreExternoLabel.Visibility = System.Windows.Visibility.Visible;
            ObservacionExternoLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void RechazoCheck(object sender, RoutedEventArgs e)
        {
        }

        private void RechazoUncheck(object sender, RoutedEventArgs e)
        {
        }

        private void AgregarActividadClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
