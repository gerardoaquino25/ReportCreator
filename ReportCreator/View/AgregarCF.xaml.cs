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
    /// Lógica de interacción para AgregarCF.xaml
    /// </summary>
    public partial class AgregarCF : UserControl
    {
        IRepository repo = new Repository();
        private bool nuevo;
        private long idEntrada;

        public AgregarCF(long idEntrada, bool nuevo)
        {
            this.nuevo = nuevo;
            this.idEntrada = idEntrada;
        }

        private bool ValidarDatos()
        {
            return true;
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {

            if (ValidarDatos())
            {
                repo.AgregarCF(NombreCampania.Text);
                MainWindow.self.Content = new EntradaCampaniaFinanciera(idEntrada, nuevo);
            }
        }

        private void VolverClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new EntradaCampaniaFinanciera(idEntrada, nuevo);
        }
    }
}
