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
    /// Lógica de interacción para AgregarPadronAporte.xaml
    /// </summary>
    public partial class AgregarPadronAporte : UserControl
    {
        private long idEntrada;
        private bool cargaInicial;
        IRepository repo = new Repository();
        private bool nuevo;
        private long campaniaAsociadaId;
        private bool entradaNueva;
        private bool padron;
        private long idPadronAporte;
        private object aportador;

        public AgregarPadronAporte()
        {
            InitializeComponent();
        }

        //public AgregarPadronAporte(long idPadronAporte, long idEntrada, long campaniaAsociadaId, bool entradaNueva, bool padron, bool nuevo)
        //{
        //    InitializeComponent();
        //    this.idEntrada = idEntrada;
        //    this.campaniaAsociadaId = campaniaAsociadaId;
        //    this.entradaNueva = entradaNueva;
        //    this.nuevo = nuevo;
        //    this.padron = padron;
        //    this.idPadronAporte = idPadronAporte;
        //    cargaInicial = true;
        //    iniciar(false);
        //}

        public AgregarPadronAporte(long idEntrada, long campaniaAsociadaId, bool entradaNueva, bool padron, bool nuevo)
        {
            InitializeComponent();
            this.idEntrada = idEntrada;
            this.campaniaAsociadaId = campaniaAsociadaId;
            this.entradaNueva = entradaNueva;
            this.nuevo = nuevo;
            this.padron = padron;
            cargaInicial = true;
            iniciar(true);
        }

        public AgregarPadronAporte(AporteCF entrada, bool entradaNueva, bool nuevo)
        {
            InitializeComponent();
            this.idEntrada = entrada.entradaCampaniaFinancieraId;
            this.campaniaAsociadaId = entrada.campaniaFinancieraId;
            this.entradaNueva = entradaNueva;
            this.nuevo = nuevo;
            this.padron = false;
            this.aportador = entrada;
            this.nuevo = nuevo;
            iniciar(false);
        }

        public AgregarPadronAporte(PadronCF entrada, bool entradaNueva, bool nuevo)
        {
            InitializeComponent();
            this.idEntrada = entrada.entradaCampaniaFinancieraId;
            this.campaniaAsociadaId = entrada.campaniaFinancieraId;
            this.entradaNueva = entradaNueva;
            this.nuevo = nuevo;
            this.padron = true;
            this.aportador = entrada;
            this.nuevo = nuevo;
            iniciar(false);
        }

        private void iniciar(bool nuevo)
        {
            IList<string> listaTipoAportante = new List<string>();
            IList<string> listaTipoAporte = new List<string>();
            int seleccionTipoAporte = 0;
            int seleccionTipoAportante = 0;

            Interno.ItemsSource = repo.ObtenerInternos();
            if (Interno.Items.Count > 0)
                listaTipoAportante.Add("Interno");
            else
                seleccionTipoAportante = 1;

            Externo.ItemsSource = repo.ObtenerExternos();
            listaTipoAportante.Add("Externo");

            if (!this.padron)
            {
                listaTipoAporte.Add("Padrón y Cuota");
                listaTipoAporte.Add("Cuota");
            }
            else
            {
                listaTipoAporte.Add("Padrón");
                listaTipoAporte.Add("Padrón y Cuota");
            }

            TipoAporte.ItemsSource = listaTipoAporte;
            TipoAportante.ItemsSource = listaTipoAportante;

            if (nuevo)
            {
                TipoAporte.SelectedIndex = seleccionTipoAporte;
                TipoAportante.SelectedItem = seleccionTipoAportante;
                if (seleccionTipoAportante == 1 && Externo.Items.Count <= 0)
                    ExternoExistente.IsChecked = false;
            }
            else
            {
                if (padron)
                {
                    TipoAportanteLabel.Visibility = System.Windows.Visibility.Collapsed;
                    TipoAportante.Visibility = System.Windows.Visibility.Collapsed;
                    PagoLabel.Visibility = System.Windows.Visibility.Collapsed;
                    Pago.Visibility = System.Windows.Visibility.Collapsed;
                    FechaAporteLabel.Visibility = System.Windows.Visibility.Collapsed;
                    FechaAporte.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    TipoAportanteLabel.Visibility = System.Windows.Visibility.Collapsed;
                    TipoAportante.Visibility = System.Windows.Visibility.Collapsed;
                    CompromisoLabel.Visibility = System.Windows.Visibility.Collapsed;
                    Compromiso.Visibility = System.Windows.Visibility.Collapsed;
                }
            }

            cargaInicial = false;
        }

        private bool ValidarDatos()
        {
            return true;
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

            TipoAportanteLabel.Visibility = System.Windows.Visibility.Visible;
            TipoAportante.Visibility = System.Windows.Visibility.Visible;
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

            TipoAportanteLabel.Visibility = System.Windows.Visibility.Visible;
            TipoAportante.Visibility = System.Windows.Visibility.Visible;
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

            CompromisoLabel.Visibility = System.Windows.Visibility.Collapsed;
            Compromiso.Visibility = System.Windows.Visibility.Collapsed;

            TipoAportanteLabel.Visibility = System.Windows.Visibility.Visible;
            TipoAportante.Visibility = System.Windows.Visibility.Visible;
            PagoLabel.Visibility = System.Windows.Visibility.Visible;
            Pago.Visibility = System.Windows.Visibility.Visible;
            FechaAporteLabel.Visibility = System.Windows.Visibility.Visible;
            FechaAporte.Visibility = System.Windows.Visibility.Visible;
        }

        private void SelectionChangedTipoAporte(object sender, SelectionChangedEventArgs e)
        {
            int suma = padron ? 0 : 1;
            switch (TipoAporte.SelectedIndex + suma)
            {
                case 0:
                    LimpiarParaPadron();
                    break;
                case 1:
                    LimpiarParaPadronYCuota();
                    break;
                case 2:
                    LimpiarParaCuota();
                    break;
            }
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {
            if (ValidarDatos())
            {
                object aportanteAux = null;
                if (NombreExterno.IsVisible)
                    aportanteAux = repo.AgregarExterno(NombreExterno.Text, ObservacionExterno.Text);

                if (TipoAportante.SelectedIndex == 0)
                    aportanteAux = Interno.SelectedItem;
                else if (TipoAportante.SelectedIndex == 1 && !NombreExterno.IsVisible)
                    aportanteAux = Externo.SelectedItem;

                switch (TipoAporte.SelectedIndex)
                {
                    case 0:
                        repo.AgregarPadronCF(new PadronCF() { aportante = aportanteAux, campaniaFinancieraId = campaniaAsociadaId, compromiso = Convert.ToInt32(Compromiso.Text), entradaCampaniaFinancieraId = idEntrada, observacion = Observacion.Text });
                        break;
                    case 1:
                        repo.AgregarPadronCF(new PadronCF() { aportante = aportanteAux, campaniaFinancieraId = campaniaAsociadaId, compromiso = Convert.ToInt32(Compromiso.Text), entradaCampaniaFinancieraId = idEntrada, observacion = Observacion.Text });
                        repo.AgregarAporteCF(new AporteCF() { aportante = aportanteAux, campaniaFinancieraId = campaniaAsociadaId, pago = Convert.ToInt32(Pago.Text), entradaCampaniaFinancieraId = idEntrada, fechaAporte = (DateTime)FechaAporte.SelectedDate, observacion = Observacion.Text });
                        break;
                    case 2:
                        repo.AgregarAporteCF(new AporteCF() { aportante = aportanteAux, campaniaFinancieraId = campaniaAsociadaId, pago = Convert.ToInt32(Pago.Text), entradaCampaniaFinancieraId = idEntrada, fechaAporte = (DateTime)FechaAporte.SelectedDate, observacion = Observacion.Text });
                        break;
                }

                MainWindow.self.Content = new EntradaCampaniaFinanciera(idEntrada, nuevo);
            }
        }

        private void SelectionChangedTipoAportante(object sender, SelectionChangedEventArgs e)
        {
            int suma = padron ? 0 : 1;
            if (!cargaInicial)
            {
                switch (TipoAportante.SelectedIndex)
                {
                    case 0:
                        switch (TipoAporte.SelectedIndex + suma)
                        {
                            case 0:
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
                            case 1:
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
                            case 2:
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
                        switch (TipoAporte.SelectedIndex + suma)
                        {
                            case 0:
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
                            case 1:
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
                            case 2:
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

        private void VolverClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new EntradaCampaniaFinanciera(idEntrada, nuevo);
        }

        private void ExternoExistenteCheck(object sender, RoutedEventArgs e)
        {
            NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            NombreExterno.Visibility = System.Windows.Visibility.Collapsed;
            ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            ObservacionExterno.Visibility = System.Windows.Visibility.Collapsed;

            ExternoLabel.Visibility = System.Windows.Visibility.Visible;
            Externo.Visibility = System.Windows.Visibility.Visible;
        }

        private void ExternoExistenteUncheck(object sender, RoutedEventArgs e)
        {
            ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            Externo.Visibility = System.Windows.Visibility.Collapsed;

            NombreExternoLabel.Visibility = System.Windows.Visibility.Visible;
            NombreExterno.Visibility = System.Windows.Visibility.Visible;
            ObservacionExternoLabel.Visibility = System.Windows.Visibility.Visible;
            ObservacionExterno.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
