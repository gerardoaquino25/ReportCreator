using ReportCreator.Entities;
using ReportCreator.Entities.UtilityObject;
using ReportCreator.Model;
using ReportCreator.View.UtilityElement;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private long? idInforme;
        private bool nuevo;
        private long idEntrada;
        EntradaCampaniaFinancieraUO entradaCampaniaFinanciera;
        IRepository repo = new Repository();

        public EntradaCampaniaFinanciera()
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
        }

        public EntradaCampaniaFinanciera(long idEntrada, bool nuevo, bool nuevaEntrada)
            : this()
        {
            this.idEntrada = idEntrada;
            this.nuevo = nuevo;
            entradaCampaniaFinanciera = repo.ObtenerEntradaCampaniaFinanciera(idEntrada);
            this.idInforme = entradaCampaniaFinanciera.informeId;
            iniciar(nuevaEntrada);
        }

        private void iniciar(bool nuevo)
        {
            CampaniaAsociada.ItemsSource = repo.ObtenerCFs("FECHA_CREACION");

            bool asignarCampania = false;

            if (CampaniaAsociada.Items.Count > 0)
                asignarCampania = true;
            else
            {
                AgregarAporteBtn.Visibility = System.Windows.Visibility.Hidden;
                AgregarPadronBtn.Visibility = System.Windows.Visibility.Hidden;
            }

            Titulo.Text = entradaCampaniaFinanciera.titulo;
            if (nuevo)
            {
                if (asignarCampania)
                    CampaniaAsociada.SelectedIndex = 0;
            }
            else
            {
                if (asignarCampania)
                {
                    if (entradaCampaniaFinanciera.campaniaFinanciera != null)
                        CampaniaAsociada.SelectedItem = entradaCampaniaFinanciera.campaniaFinanciera;
                    else
                    {
                        CampaniaAsociada.SelectedIndex = 0;
                        entradaCampaniaFinanciera.campaniaFinanciera = (CampaniaFinanciera)CampaniaAsociada.SelectedItem;
                    }
                }

            }

            if (asignarCampania)
            {
                entradaCampaniaFinanciera.padrones = repo.ObtenerPadronesCF((long)entradaCampaniaFinanciera.id, ((CampaniaFinanciera)CampaniaAsociada.SelectedItem).id);
                entradaCampaniaFinanciera.aportes = repo.ObtenerAportesCF((long)entradaCampaniaFinanciera.id, ((CampaniaFinanciera)CampaniaAsociada.SelectedItem).id);
            }

            Padrones.ItemsSource = entradaCampaniaFinanciera.padrones;
            Aportes.ItemsSource = entradaCampaniaFinanciera.aportes;
        }

        private void AgregarPadronClick(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new AgregarPadronAporte(idEntrada, ((CampaniaFinanciera)CampaniaAsociada.SelectedItem).id, true, true, nuevo));
        }

        private void AgregarAporteClick(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new AgregarPadronAporte(idEntrada, ((CampaniaFinanciera)CampaniaAsociada.SelectedItem).id, true, false, nuevo));
        }

        private void AgregarCFClick(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new AgregarCF(idEntrada, nuevo));
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {

            if (ValidarDatos())
            {
                entradaCampaniaFinanciera.titulo = Titulo.Text;
                entradaCampaniaFinanciera.campaniaFinanciera = (CampaniaFinanciera)CampaniaAsociada.SelectedItem;
                repo.GuardarEntradaCampaniaFinanciera(entradaCampaniaFinanciera);
                MainWindow.SetContent(new Borrador((long)idInforme, nuevo));
            }
        }

        private bool ValidarDatos()
        {
            return true;
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new Borrador((long)idInforme, nuevo));
        }

        private void RowPadronesKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Delete == e.Key)
            {
                DataGridRow row = sender as DataGridRow;
                PadronCF entrada = (PadronCF)row.Item;

                if (entrada.entradaCampaniaFinancieraId == entradaCampaniaFinanciera.id)
                {
                    entradaCampaniaFinanciera.padrones.Remove(entrada);
                    //repo.BorrarEntrada(entrada.id, entrada.tipo.id);
                }
            }
        }

        private void RowAportesKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Delete == e.Key)
            {
                DataGridRow row = sender as DataGridRow;
                AporteCF entrada = (AporteCF)row.Item;
                if (entrada.entradaCampaniaFinancieraId == entradaCampaniaFinanciera.id)
                {
                    entradaCampaniaFinanciera.aportes.Remove(entrada);
                    //repo.BorrarEntrada(entrada.id, entrada.tipo.id);
                }
            }
        }

        private void RowPadronesDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            PadronCF entrada = (PadronCF)row.Item;
            if (entrada.entradaCampaniaFinancieraId == entradaCampaniaFinanciera.id)
                MainWindow.SetContent(new AgregarPadronAporte(entrada, false, nuevo));
        }

        private void RowAportesDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            AporteCF entrada = (AporteCF)row.Item;
            if (entrada.entradaCampaniaFinancieraId == entradaCampaniaFinanciera.id)
                MainWindow.SetContent(new AgregarPadronAporte(entrada, false, nuevo));
        }
    }
}
