using ReportCreator.Entities.UtilityObject;
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
        private EntradaGenericaUO entradaGenerica;
        IRepository repo = new Repository();
        private bool nuevo;

        public EntradaGenerica(long idEntrada, bool nuevo)
        {
            InitializeComponent();
            this.entradaGenerica = repo.ObtenerEntradaGenerica(idEntrada);
            iniciar();
            Texto.Text = entradaGenerica.data;
            this.nuevo = nuevo;
        }

        public EntradaGenerica(long idInforme, string titulo, bool nuevo)
        {
            InitializeComponent();
            iniciar();
            entradaGenerica.informeId = idInforme;
            entradaGenerica.titulo = titulo;
            this.nuevo = nuevo;
        }

        private void iniciar()
        {
            if (entradaGenerica == null)
                entradaGenerica = new EntradaGenericaUO();
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {
            if (entradaGenerica.id == 0)
                entradaGenerica.id = repo.AgregarEntrada((long)entradaGenerica.informeId, entradaGenerica.titulo, 1);
            entradaGenerica.data = Texto.Text;
            repo.GuardarEntradaGenerica(entradaGenerica);
            MainWindow.self.Content = new Borrador((long)entradaGenerica.informeId, nuevo);
        }

        private void CancelarClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Borrador((long)entradaGenerica.informeId, nuevo);
        }
    }
}
