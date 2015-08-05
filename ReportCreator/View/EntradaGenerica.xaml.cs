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
    /// Interaction logic for AMEntrada.xaml
    /// </summary>
    public partial class EntradaGenerica : UserControl
    {
        private long idInforme;
        private long idEntrada;

        public EntradaGenerica()
        {
            InitializeComponent();
        }

        public EntradaGenerica(long idInforme, long idEntrada)
        {
            InitializeComponent();
            this.idInforme = idInforme;
            this.idEntrada = idEntrada;
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {
            IRepository repo = new Repository();
            repo.GuardarEntradaGenerica(idEntrada, Texto.Text);
            MainWindow.self.Content = new NuevoBorrador(idInforme);
        }

        private void CancelarClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new NuevoBorrador();
        }
    }
}
