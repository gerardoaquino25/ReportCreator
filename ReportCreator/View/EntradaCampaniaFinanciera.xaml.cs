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
    /// Lógica de interacción para EntradaCampaniaFinanciera.xaml
    /// </summary>
    public partial class EntradaCampaniaFinanciera : UserControl
    {
        private long idInforme;
        private bool nuevo;
        private long idEntrada;
        private bool cargaInicial = true;
        Entities.EntradaCampaniaFinanciera entradaCampaniaFinanciera;
        IRepository repo = new Repository();

        //public EntradaCampaniaFinanciera(long idEntrada, bool nuevo)
        //{
        //    InitializeComponent();
        //    //CampaniaAsociadaLabel.Visibility = System.Windows.Visibility.Collapsed;
        //    //CampaniaAsociada.Visibility = System.Windows.Visibility.Collapsed;
        //    //InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
        //    //Interno.Visibility = System.Windows.Visibility.Collapsed;
        //    //ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
        //    //Externo.Visibility = System.Windows.Visibility.Collapsed;
        //    //NombreLabel.Visibility = System.Windows.Visibility.Collapsed;
        //    //Nombre.Visibility = System.Windows.Visibility.Collapsed;
        //    this.cotizacion = repo.ObtenerEntradaCampaniaFinanciera(idEntrada);
        //    this.idEntrada = idEntrada;
        //    this.nuevo = nuevo;
        //}

        public EntradaCampaniaFinanciera(long idEntrada, bool nuevo)
        {
            InitializeComponent();
            this.idEntrada = idEntrada;
            this.nuevo = nuevo;
            entradaCampaniaFinanciera = repo.ObtenerEntradaCampaniaFinanciera(idEntrada);
            this.idInforme = entradaCampaniaFinanciera.idInforme;
            iniciar(true);
        }

        private void iniciar(bool nuevo)
        {
            CampaniaAsociada.ItemsSource = repo.ObtenerCFs("FECHA_CREACION");
            Aportes.ItemsSource = repo.ObtenerAportesCF(idEntrada);
            Padrones.ItemsSource = repo.ObtenerPadronesCF(idEntrada);

            bool asignarCampania = false;

            if (CampaniaAsociada.Items.Count > 0)
                asignarCampania = true;
            else
            {
                AgregarAporteBtn.Visibility = System.Windows.Visibility.Hidden;
                AgregarCFBtn.Visibility = System.Windows.Visibility.Hidden;
            }

            if (nuevo)
            {
                if (asignarCampania)
                    CampaniaAsociada.SelectedIndex = 0;
            }
            else
            {
                Titulo.Text = entradaCampaniaFinanciera.titulo;
                CampaniaAsociada.SelectedItem = entradaCampaniaFinanciera.campaniaFinanciera;
            }

            cargaInicial = false;
        }

        private void AgregarPadron(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new AgregarPadronAporte(idEntrada, ((CampaniaFinanciera)CampaniaAsociada.SelectedItem).id, true, true, nuevo);
        }

        private void AgregarAporte(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new AgregarPadronAporte(idEntrada, ((CampaniaFinanciera)CampaniaAsociada.SelectedItem).id, true, false, nuevo);
        }

        private void AgregarCF(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new AgregarCF(idEntrada, nuevo);
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {

            if (ValidarDatos())
            {
                MainWindow.self.Content = new Borrador(idInforme, nuevo);
            }
        }

        private bool ValidarDatos()
        {
            return true;
        }

        private void VolverClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Borrador(idInforme, nuevo);
        }

        private void RowPadronesKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Delete == e.Key)
            {
                DataGridRow row = sender as DataGridRow;
                PadronCF entrada = (PadronCF)row.Item;
                //repo.BorrarEntrada(entrada.id, entrada.tipo.id);
                //entradas.Remove(entrada);
            }
        }

        private void RowAportesKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Delete == e.Key)
            {
                DataGridRow row = sender as DataGridRow;
                AporteCF entrada = (AporteCF)row.Item;
                //repo.BorrarEntrada(entrada.id, entrada.tipo.id);
                //entradas.Remove(entrada);
            }
        }

        private void RowPadronesDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            PadronCF entrada = (PadronCF)row.Item;
            MainWindow.self.Content = new AgregarPadronAporte(entrada, false, nuevo);
        }

        private void RowAportesDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            AporteCF entrada = (AporteCF)row.Item;
            MainWindow.self.Content = new AgregarPadronAporte(entrada, false, nuevo);
        }
    }
}
