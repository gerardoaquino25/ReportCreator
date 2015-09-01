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
        //private long idPadronAporte;
        private object entrada;
        private bool ceroInterno = true;

        //public AgregarPadronAporte()
        //{
        //    InitializeComponent();
        //}

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

        public AgregarPadronAporte(AporteCF aportador, bool entradaNueva, bool nuevo)
        {
            InitializeComponent();
            this.idEntrada = aportador.entradaCampaniaFinancieraId;
            this.campaniaAsociadaId = aportador.campaniaFinancieraId;
            this.entradaNueva = entradaNueva;
            this.nuevo = nuevo;
            this.padron = false;
            this.entrada = aportador;
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
            this.entrada = entrada;
            this.nuevo = nuevo;
            iniciar(false);
        }

        private void iniciar(bool nuevo)
        {
            IList<string> listaTipoAportante = new List<string>();
            IList<string> listaTipoAporte = new List<string>();
            int seleccionTipoAporte = 0;
            int seleccionTipoAportante = 0;
            int resta = 1;

            Interno.ItemsSource = repo.ObtenerInternos();
            if (Interno.Items.Count > 0)
            {
                ceroInterno = false;
                resta = 0;
                listaTipoAportante.Add("Interno");
            }

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
                TipoAportante.SelectedIndex = seleccionTipoAportante;
                if (seleccionTipoAportante == (1 - resta) && Externo.Items.Count <= 0)
                {
                    ExternoExistenteUncheck(null, null);
                    ExternoExistente.IsChecked = false;
                }
            }
            else
            {
                TipoAportanteLabel.Visibility = System.Windows.Visibility.Collapsed;
                TipoAporteLabel.Visibility = System.Windows.Visibility.Collapsed;
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ExternoExistente.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                if (padron)
                {
                    RechazoLabel.Visibility = System.Windows.Visibility.Collapsed;
                    PagoLabel.Visibility = System.Windows.Visibility.Collapsed;
                    FechaAporteLabel.Visibility = System.Windows.Visibility.Collapsed;

                    PadronCF padronAux = (PadronCF)entrada;
                    if (padronAux.aportante.GetType() == typeof(Interno))
                    {
                        ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                        Interno.SelectedItem = padronAux.aportante;
                    }
                    else if (padronAux.aportante.GetType() == typeof(Externo))
                    {
                        NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                        ExternoExistente.Visibility = System.Windows.Visibility.Collapsed;
                        ObservacionExterno.Visibility = System.Windows.Visibility.Collapsed;
                        InternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                        Externo.SelectedItem = padronAux.aportante;
                    }
                    Compromiso.Text = padronAux.compromiso.ToString();
                    Observacion.Text = padronAux.observacion;
                }
                else
                {
                    TipoAportanteLabel.Visibility = System.Windows.Visibility.Collapsed;
                    CompromisoLabel.Visibility = System.Windows.Visibility.Collapsed;

                    AporteCF aporteAux = (AporteCF)entrada;

                    if (aporteAux.aportante.GetType() == typeof(Interno))
                    {
                        ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                        Interno.SelectedItem = aporteAux.aportante;
                    }
                    else if (aporteAux.aportante.GetType() == typeof(Externo))
                    {
                        NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                        ExternoExistente.Visibility = System.Windows.Visibility.Collapsed;
                        ObservacionExterno.Visibility = System.Windows.Visibility.Collapsed;
                        InternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                        Externo.SelectedItem = aporteAux.aportante;
                    }

                    FechaAporte.SelectedDate = aporteAux.fechaAporte;
                    Pago.Text = aporteAux.pago.ToString();
                    Observacion.Text = aporteAux.observacion;
                    Rechazo.IsChecked = aporteAux.rechazo;
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
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Collapsed;

                InternoLabel.Visibility = System.Windows.Visibility.Visible;
            }
            else if (Externo.Items.Count > 0)
            {
                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                TipoAportante.SelectedIndex = 1;
                ExternoExistente.IsChecked = true;
                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                TipoAportante.SelectedIndex = 1;
                ExternoExistente.IsChecked = false;
                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Visible;
            }

            PagoLabel.Visibility = System.Windows.Visibility.Collapsed;
            RechazoLabel.Visibility = System.Windows.Visibility.Collapsed;
            FechaAporteLabel.Visibility = System.Windows.Visibility.Collapsed;

            TipoAportanteLabel.Visibility = System.Windows.Visibility.Visible;
            CompromisoLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void LimpiarParaPadronYCuota()
        {
            if (Interno.Items.Count > 0)
            {
                TipoAportante.SelectedIndex = 0;
                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Collapsed;

                InternoLabel.Visibility = System.Windows.Visibility.Visible;
            }
            else if (Externo.Items.Count > 0)
            {
                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                TipoAportante.SelectedIndex = 1;
                ExternoExistente.IsChecked = true;
                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                TipoAportante.SelectedIndex = 1;
                ExternoExistente.IsChecked = false;
                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Visible;
            }

            TipoAportanteLabel.Visibility = System.Windows.Visibility.Visible;
            CompromisoLabel.Visibility = System.Windows.Visibility.Visible;
            PagoLabel.Visibility = System.Windows.Visibility.Visible;
            RechazoLabel.Visibility = System.Windows.Visibility.Visible;
            Rechazo.IsChecked = false;
            FechaAporteLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void LimpiarParaCuota()
        {
            if (Interno.Items.Count > 0)
            {
                TipoAportante.SelectedIndex = 0;
                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Collapsed;

                InternoLabel.Visibility = System.Windows.Visibility.Visible;
            }
            else if (Externo.Items.Count > 0)
            {
                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                TipoAportante.SelectedIndex = 1;
                ExternoExistente.IsChecked = true;
                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                TipoAportante.SelectedIndex = 1;
                ExternoExistente.IsChecked = false;
                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Visible;
            }

            CompromisoLabel.Visibility = System.Windows.Visibility.Collapsed;

            TipoAportanteLabel.Visibility = System.Windows.Visibility.Visible;
            PagoLabel.Visibility = System.Windows.Visibility.Visible;
            RechazoLabel.Visibility = System.Windows.Visibility.Visible;
            Rechazo.IsChecked = false;
            FechaAporteLabel.Visibility = System.Windows.Visibility.Visible;
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
                if (entrada == null)
                {
                    int suma = padron ? 0 : 1;
                    object aportanteAux = null;
                    if (NombreExterno.IsVisible)
                        aportanteAux = repo.AgregarExterno(NombreExterno.Text, ObservacionExterno.Text, (Interno)Interno.SelectedItem);

                    if (TipoAportante.SelectedIndex == 0)
                        aportanteAux = Interno.SelectedItem;
                    else if (TipoAportante.SelectedIndex == 1 && !NombreExterno.IsVisible)
                        aportanteAux = Externo.SelectedItem;

                    switch (TipoAporte.SelectedIndex + suma)
                    {
                        case 0:
                            repo.AgregarPadronCF(new PadronCF() { aportante = aportanteAux, campaniaFinancieraId = campaniaAsociadaId, compromiso = Convert.ToInt32(Compromiso.Text), entradaCampaniaFinancieraId = idEntrada, observacion = Observacion.Text });
                            break;
                        case 1:
                            repo.AgregarPadronCF(new PadronCF() { aportante = aportanteAux, campaniaFinancieraId = campaniaAsociadaId, compromiso = Convert.ToInt32(Compromiso.Text), entradaCampaniaFinancieraId = idEntrada, observacion = Observacion.Text });
                            repo.AgregarAporteCF(new AporteCF() { aportante = aportanteAux, campaniaFinancieraId = campaniaAsociadaId, pago = String.IsNullOrEmpty(Pago.Text) ? 0 : Convert.ToInt32(Pago.Text), entradaCampaniaFinancieraId = idEntrada, fechaAporte = (DateTime)FechaAporte.SelectedDate, observacion = Observacion.Text, rechazo = (bool)Rechazo.IsChecked });
                            break;
                        case 2:
                            repo.AgregarAporteCF(new AporteCF() { aportante = aportanteAux, campaniaFinancieraId = campaniaAsociadaId, pago = String.IsNullOrEmpty(Pago.Text) ? 0 : Convert.ToInt32(Pago.Text), entradaCampaniaFinancieraId = idEntrada, fechaAporte = (DateTime)FechaAporte.SelectedDate, observacion = Observacion.Text, rechazo = (bool)Rechazo.IsChecked });
                            break;
                    }
                }
                else
                {
                    if (padron)
                    {
                        PadronCF padronAux = (PadronCF)entrada;
                        padronAux.compromiso = Convert.ToInt32(Compromiso.Text);
                        padronAux.observacion = Observacion.Text;

                        if (padronAux.aportante.GetType() == typeof(Interno))
                            padronAux.aportante = Interno.SelectedItem;
                        else if (padronAux.aportante.GetType() == typeof(Interno))
                            padronAux.aportante = Externo.SelectedItem;

                        repo.GuardarPadronCF(padronAux);
                    }
                    else
                    {
                        AporteCF aporteAux = (AporteCF)entrada;
                        aporteAux.pago = String.IsNullOrEmpty(Pago.Text) ? 0 : Convert.ToInt32(Pago.Text);
                        aporteAux.observacion = Observacion.Text;
                        aporteAux.rechazo = (bool)Rechazo.IsChecked;
                        aporteAux.fechaAporte = (DateTime)FechaAporte.SelectedDate;

                        if (aporteAux.aportante.GetType() == typeof(Interno))
                            aporteAux.aportante = Interno.SelectedItem;
                        else if (aporteAux.aportante.GetType() == typeof(Interno))
                            aporteAux.aportante = Externo.SelectedItem;

                        repo.GuardarAporteCF(aporteAux);
                    }
                }

                MainWindow.SetContent(new EntradaCampaniaFinanciera(idEntrada, nuevo, false));
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
                                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Collapsed;
                                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                PagoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                RechazoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                                CompromisoLabel.Visibility = System.Windows.Visibility.Visible;
                                InternoLabel.Visibility = System.Windows.Visibility.Visible;
                                break;
                            case 1:
                                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Collapsed;
                                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                                CompromisoLabel.Visibility = System.Windows.Visibility.Visible;
                                PagoLabel.Visibility = System.Windows.Visibility.Visible;
                                RechazoLabel.Visibility = System.Windows.Visibility.Visible;
                                Rechazo.IsChecked = false;
                                InternoLabel.Visibility = System.Windows.Visibility.Visible;
                                break;
                            case 2:
                                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Collapsed;
                                ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                CompromisoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                                PagoLabel.Visibility = System.Windows.Visibility.Visible;
                                RechazoLabel.Visibility = System.Windows.Visibility.Visible;
                                Rechazo.IsChecked = false;
                                InternoLabel.Visibility = System.Windows.Visibility.Visible;
                                break;
                        }
                        break;
                    case 1:
                        switch (TipoAporte.SelectedIndex + suma)
                        {
                            case 0:
                                PagoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                RechazoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                                ExternoExistente.IsChecked = true;
                                CompromisoLabel.Visibility = System.Windows.Visibility.Visible;
                                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Visible;
                                ExternoLabel.Visibility = System.Windows.Visibility.Visible;
                                break;
                            case 1:
                                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                                ExternoExistente.IsChecked = true;
                                CompromisoLabel.Visibility = System.Windows.Visibility.Visible;
                                PagoLabel.Visibility = System.Windows.Visibility.Visible;
                                RechazoLabel.Visibility = System.Windows.Visibility.Visible;
                                Rechazo.IsChecked = false;
                                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Visible;
                                ExternoLabel.Visibility = System.Windows.Visibility.Visible;
                                break;
                            case 2:
                                CompromisoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                InternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                                NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

                                ExternoExistente.IsChecked = true;
                                PagoLabel.Visibility = System.Windows.Visibility.Visible;
                                RechazoLabel.Visibility = System.Windows.Visibility.Visible;
                                Rechazo.IsChecked = false;
                                ExternoExistenteLabel.Visibility = System.Windows.Visibility.Visible;
                                ExternoLabel.Visibility = System.Windows.Visibility.Visible;
                                break;
                        }
                        break;
                }
            }
        }

        private void VolverClick(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new EntradaCampaniaFinanciera(idEntrada, nuevo, false));
        }

        private void ExternoExistenteCheck(object sender, RoutedEventArgs e)
        {
            NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            InternoLabel.Visibility = System.Windows.Visibility.Collapsed;

            ExternoLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void ExternoExistenteUncheck(object sender, RoutedEventArgs e)
        {
            ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

            InternoLabel.Visibility = System.Windows.Visibility.Visible;
            NombreExternoLabel.Visibility = System.Windows.Visibility.Visible;
            ObservacionExternoLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void RechazoCheck(object sender, RoutedEventArgs e)
        {
            PagoLabel.Visibility = System.Windows.Visibility.Collapsed;
            Pago.Text = "";
        }

        private void RechazoUncheck(object sender, RoutedEventArgs e)
        {
            PagoLabel.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
