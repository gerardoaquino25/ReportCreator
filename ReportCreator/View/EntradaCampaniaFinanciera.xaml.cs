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
        private string asunto;
        private bool nuevaEntrada;
        private long idEntrada;
        private bool cargaInicial = true;
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

        public EntradaCampaniaFinanciera(long idInforme, string asunto, bool nuevo)
        {
            InitializeComponent();
            this.idInforme = idInforme;
            this.asunto = asunto;
            this.nuevaEntrada = nuevo;
            iniciar(true);
        }

        private void iniciar(bool nuevo)
        {
            CampaniaAsociada.ItemsSource = repo.ObtenerCFs("FECHA_CREACION");
            IList<string> listaTipoAportante = new List<string>();
            IList<string> listaTipoAporte = new List<string>();
            int seleccionTipoAporte = 0;
            int seleccionTipoAportante = 0;
            bool asignarCampania = false;

            listaTipoAporte.Add("Campaña");
            if (CampaniaAsociada.Items.Count > 0)
            {
                asignarCampania = true;
                CampaniaFinanciera primerCampania = ((CampaniaFinanciera)CampaniaAsociada.Items[0]);
                if (DateTime.UtcNow.AddMonths(-6) < primerCampania.fechaCreacion)
                {
                    seleccionTipoAporte = 1;
                    if (Interno.Items.Count <= 0)
                        seleccionTipoAportante = 1;
                }

                Interno.ItemsSource = repo.ObtenerInternos();
                if (Interno.Items.Count > 0)
                    listaTipoAportante.Add("Interno");

                Externo.ItemsSource = repo.ObtenerExternos();
                listaTipoAportante.Add("Externo");



                listaTipoAporte.Add("Padrón");
                listaTipoAporte.Add("Padrón y Cuota");

                if (Interno.Items.Count > 0 || Externo.Items.Count > 0)
                    listaTipoAporte.Add("Cuota");
            }

            TipoAporte.ItemsSource = listaTipoAporte;
            TipoAportante.ItemsSource = listaTipoAportante;

            if (nuevo)
            {
                TipoAporte.SelectedIndex = seleccionTipoAporte;
                if (asignarCampania)
                    CampaniaAsociada.SelectedIndex = 0;
                else
                {
                    TipoAportante.SelectedItem = seleccionTipoAportante;
                    if (seleccionTipoAportante == 1 && Externo.Items.Count <= 0)
                        ExternoExistente.IsChecked = false;
                }
            }
            else
            {

            }

            cargaInicial = false;
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {
            if (NombreExterno.IsVisible)
                repo.AgregarExterno(NombreExterno.Text, ObservacionExterno.Text);

            if (TipoAporte.SelectedIndex != 0)
            {

            }

            switch (TipoAporte.SelectedIndex)
            {
                case 0:
                    repo.AgregarCF(NombreCampania.Text);
                    break;
                case 1:
                    repo.AgregarPadronCF(NombreCampania.Text);
                    break;
                case 2:
                    repo.AgregarPadronCF(NombreCampania.Text);
                    repo.AgregarAporteCF(NombreCampania.Text);
                    break;
                case 3:
                    repo.AgregarAporteCF(NombreCampania.Text);
                    break;
            }

            MainWindow.self.Content = new Borrador(idInforme, nuevaEntrada);
        }

        private void VolverClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Borrador(idInforme, nuevaEntrada);
        }

        private void LimpiarParaCampania()
        {
            CampaniaAsociadaLabel.Visibility = System.Windows.Visibility.Collapsed;
            CampaniaAsociada.Visibility = System.Windows.Visibility.Collapsed;
            InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            Interno.Visibility = System.Windows.Visibility.Collapsed;
            ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            Externo.Visibility = System.Windows.Visibility.Collapsed;
            PagoLabel.Visibility = System.Windows.Visibility.Collapsed;
            Pago.Visibility = System.Windows.Visibility.Collapsed;
            NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            NombreExterno.Visibility = System.Windows.Visibility.Collapsed;
            FechaAporteLabel.Visibility = System.Windows.Visibility.Collapsed;
            FechaAporte.Visibility = System.Windows.Visibility.Collapsed;
            TipoAportanteLabel.Visibility = System.Windows.Visibility.Collapsed;
            TipoAportante.Visibility = System.Windows.Visibility.Collapsed;
            ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            ObservacionExterno.Visibility = System.Windows.Visibility.Collapsed;
            ObservacionLabel.Visibility = System.Windows.Visibility.Collapsed;
            Observacion.Visibility = System.Windows.Visibility.Collapsed;
            ExternoExistente.Visibility = System.Windows.Visibility.Collapsed;
            CompromisoLabel.Visibility = System.Windows.Visibility.Collapsed;
            Compromiso.Visibility = System.Windows.Visibility.Collapsed;

            NombreCampaniaLabel.Visibility = System.Windows.Visibility.Visible;
            NombreCampania.Visibility = System.Windows.Visibility.Visible;
            NombreCampania.Text = "";
        }

        private void LimpiarParaPadron()
        {
            if (Interno.Items.Count > 0)
            {
                TipoAportante.SelectedIndex = 0;
                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                Externo.Visibility = System.Windows.Visibility.Collapsed;
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                NombreExterno.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExterno.Visibility = System.Windows.Visibility.Collapsed;
                ExternoExistente.Visibility = System.Windows.Visibility.Collapsed;

                InternoLabel.Visibility = System.Windows.Visibility.Visible;
                Interno.Visibility = System.Windows.Visibility.Visible;
            }
            else if (Externo.Items.Count > 0)
            {
                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                Interno.Visibility = System.Windows.Visibility.Collapsed;
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                NombreExterno.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExterno.Visibility = System.Windows.Visibility.Collapsed;

                TipoAportante.SelectedIndex = 1;
                ExternoExistente.IsChecked = true;
                ExternoExistente.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                Interno.Visibility = System.Windows.Visibility.Collapsed;
                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                Externo.Visibility = System.Windows.Visibility.Collapsed;

                TipoAportante.SelectedIndex = 1;
                ExternoExistente.IsChecked = false;
                ExternoExistente.Visibility = System.Windows.Visibility.Visible;
            }

            PagoLabel.Visibility = System.Windows.Visibility.Collapsed;
            Pago.Visibility = System.Windows.Visibility.Collapsed;
            FechaAporteLabel.Visibility = System.Windows.Visibility.Collapsed;
            FechaAporte.Visibility = System.Windows.Visibility.Collapsed;
            NombreCampaniaLabel.Visibility = System.Windows.Visibility.Collapsed;
            NombreCampania.Visibility = System.Windows.Visibility.Collapsed;

            TipoAportanteLabel.Visibility = System.Windows.Visibility.Visible;
            TipoAportante.Visibility = System.Windows.Visibility.Visible;
            CampaniaAsociadaLabel.Visibility = System.Windows.Visibility.Visible;
            CampaniaAsociada.Visibility = System.Windows.Visibility.Visible;
            CompromisoLabel.Visibility = System.Windows.Visibility.Visible;
            Compromiso.Visibility = System.Windows.Visibility.Visible;
        }

        private void LimpiarParaPadronYCuota()
        {
            if (Interno.Items.Count > 0)
            {
                TipoAportante.SelectedIndex = 0;
                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                Externo.Visibility = System.Windows.Visibility.Collapsed;
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                NombreExterno.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExterno.Visibility = System.Windows.Visibility.Collapsed;

                InternoLabel.Visibility = System.Windows.Visibility.Visible;
                Interno.Visibility = System.Windows.Visibility.Visible;
            }
            else if (Externo.Items.Count > 0)
            {
                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                Interno.Visibility = System.Windows.Visibility.Collapsed;
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                NombreExterno.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExterno.Visibility = System.Windows.Visibility.Collapsed;

                TipoAportante.SelectedIndex = 1;
                ExternoExistente.IsChecked = true;
                ExternoExistente.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                Interno.Visibility = System.Windows.Visibility.Collapsed;
                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                Externo.Visibility = System.Windows.Visibility.Collapsed;

                TipoAportante.SelectedIndex = 1;
                ExternoExistente.IsChecked = false;
                ExternoExistente.Visibility = System.Windows.Visibility.Visible;
            }

            NombreCampaniaLabel.Visibility = System.Windows.Visibility.Collapsed;
            NombreCampania.Visibility = System.Windows.Visibility.Collapsed;

            TipoAportanteLabel.Visibility = System.Windows.Visibility.Visible;
            TipoAportante.Visibility = System.Windows.Visibility.Visible;
            CampaniaAsociadaLabel.Visibility = System.Windows.Visibility.Visible;
            CampaniaAsociada.Visibility = System.Windows.Visibility.Visible;
            CompromisoLabel.Visibility = System.Windows.Visibility.Visible;
            Compromiso.Visibility = System.Windows.Visibility.Visible;
            PagoLabel.Visibility = System.Windows.Visibility.Visible;
            Pago.Visibility = System.Windows.Visibility.Visible;
            FechaAporteLabel.Visibility = System.Windows.Visibility.Visible;
            FechaAporte.Visibility = System.Windows.Visibility.Visible;
        }

        private void LimpiarParaCuota()
        {
            if (Interno.Items.Count > 0)
            {
                TipoAportante.SelectedIndex = 0;
                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                Externo.Visibility = System.Windows.Visibility.Collapsed;
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                NombreExterno.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExterno.Visibility = System.Windows.Visibility.Collapsed;
                ExternoExistente.Visibility = System.Windows.Visibility.Collapsed;

                InternoLabel.Visibility = System.Windows.Visibility.Visible;
                Interno.Visibility = System.Windows.Visibility.Visible;
            }
            else if (Externo.Items.Count > 0)
            {
                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                Interno.Visibility = System.Windows.Visibility.Collapsed;
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                NombreExterno.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExterno.Visibility = System.Windows.Visibility.Collapsed;

                TipoAportante.SelectedIndex = 1;
                ExternoExistente.IsChecked = true;
                ExternoExistente.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                Interno.Visibility = System.Windows.Visibility.Collapsed;
                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                Externo.Visibility = System.Windows.Visibility.Collapsed;

                TipoAportante.SelectedIndex = 1;
                ExternoExistente.IsChecked = false;
                ExternoExistente.Visibility = System.Windows.Visibility.Visible;
            }

            NombreCampaniaLabel.Visibility = System.Windows.Visibility.Collapsed;
            NombreCampania.Visibility = System.Windows.Visibility.Collapsed;
            CompromisoLabel.Visibility = System.Windows.Visibility.Collapsed;
            Compromiso.Visibility = System.Windows.Visibility.Collapsed;

            TipoAportanteLabel.Visibility = System.Windows.Visibility.Visible;
            TipoAportante.Visibility = System.Windows.Visibility.Visible;
            CampaniaAsociadaLabel.Visibility = System.Windows.Visibility.Visible;
            CampaniaAsociada.Visibility = System.Windows.Visibility.Visible;
            PagoLabel.Visibility = System.Windows.Visibility.Visible;
            Pago.Visibility = System.Windows.Visibility.Visible;
            FechaAporteLabel.Visibility = System.Windows.Visibility.Visible;
            FechaAporte.Visibility = System.Windows.Visibility.Visible;
        }

        private void SelectionChangedTipoAporte(object sender, SelectionChangedEventArgs e)
        {
            switch (TipoAporte.SelectedIndex)
            {
                case 0:
                    LimpiarParaCampania();
                    break;
                case 1:
                    LimpiarParaPadron();
                    break;
                case 2:
                    LimpiarParaPadronYCuota();
                    break;
                case 3:
                    LimpiarParaCuota();
                    break;
            }
        }

        private void SelectionChangedTipoAportante(object sender, SelectionChangedEventArgs e)
        {
            if (!cargaInicial)
            {
                switch (TipoAportante.SelectedIndex)
                {
                    case 0:
                        switch (TipoAporte.SelectedIndex)
                        {
                            case 1:
                                ExternoExistente.Visibility = System.Windows.Visibility.Collapsed;
                                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                Externo.Visibility = System.Windows.Visibility.Collapsed;
                                PagoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                Pago.Visibility = System.Windows.Visibility.Collapsed;

                                CompromisoLabel.Visibility = System.Windows.Visibility.Visible;
                                Compromiso.Visibility = System.Windows.Visibility.Visible;
                                InternoLabel.Visibility = System.Windows.Visibility.Visible;
                                Interno.Visibility = System.Windows.Visibility.Visible;
                                break;
                            case 2:
                                ExternoExistente.Visibility = System.Windows.Visibility.Collapsed;
                                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                Externo.Visibility = System.Windows.Visibility.Collapsed;

                                CompromisoLabel.Visibility = System.Windows.Visibility.Visible;
                                Compromiso.Visibility = System.Windows.Visibility.Visible;
                                PagoLabel.Visibility = System.Windows.Visibility.Visible;
                                Pago.Visibility = System.Windows.Visibility.Visible;
                                InternoLabel.Visibility = System.Windows.Visibility.Visible;
                                Interno.Visibility = System.Windows.Visibility.Visible;
                                break;
                            case 3:
                                ExternoExistente.Visibility = System.Windows.Visibility.Collapsed;
                                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                Externo.Visibility = System.Windows.Visibility.Collapsed;
                                CompromisoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                Compromiso.Visibility = System.Windows.Visibility.Collapsed;

                                PagoLabel.Visibility = System.Windows.Visibility.Visible;
                                Pago.Visibility = System.Windows.Visibility.Visible;
                                InternoLabel.Visibility = System.Windows.Visibility.Visible;
                                Interno.Visibility = System.Windows.Visibility.Visible;
                                break;
                        }
                        break;
                    case 1:
                        switch (TipoAporte.SelectedIndex)
                        {
                            case 1:
                                PagoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                Pago.Visibility = System.Windows.Visibility.Collapsed;
                                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                Interno.Visibility = System.Windows.Visibility.Collapsed;

                                ExternoExistente.IsChecked = true;
                                CompromisoLabel.Visibility = System.Windows.Visibility.Visible;
                                Compromiso.Visibility = System.Windows.Visibility.Visible;
                                ExternoExistente.Visibility = System.Windows.Visibility.Visible;
                                ExternoLabel.Visibility = System.Windows.Visibility.Visible;
                                Externo.Visibility = System.Windows.Visibility.Visible;
                                break;
                            case 2:
                                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                Interno.Visibility = System.Windows.Visibility.Collapsed;

                                ExternoExistente.IsChecked = true;
                                CompromisoLabel.Visibility = System.Windows.Visibility.Visible;
                                Compromiso.Visibility = System.Windows.Visibility.Visible;
                                PagoLabel.Visibility = System.Windows.Visibility.Visible;
                                Pago.Visibility = System.Windows.Visibility.Visible;
                                ExternoExistente.Visibility = System.Windows.Visibility.Visible;
                                ExternoLabel.Visibility = System.Windows.Visibility.Visible;
                                Externo.Visibility = System.Windows.Visibility.Visible;
                                break;
                            case 3:
                                CompromisoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                Compromiso.Visibility = System.Windows.Visibility.Collapsed;
                                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                Interno.Visibility = System.Windows.Visibility.Collapsed;

                                ExternoExistente.IsChecked = true;
                                PagoLabel.Visibility = System.Windows.Visibility.Visible;
                                Pago.Visibility = System.Windows.Visibility.Visible;
                                ExternoExistente.Visibility = System.Windows.Visibility.Visible;
                                ExternoLabel.Visibility = System.Windows.Visibility.Visible;
                                Externo.Visibility = System.Windows.Visibility.Visible;
                                break;
                        }
                        break;
                }
            }
        }
    }
}
