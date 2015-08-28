using ReportCreator.Entities;
using ReportCreator.Entities.UtilityObject;
using ReportCreator.Model;
using ReportCreator.Utilities;
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
        public const string AGREGAR_PRENSA = "AGREGAR_PRENSA";
        public const string ABM_ACTIVIDAD = "ABM_ACTIVIDAD";

        private static string FALTA_COMPLETAR_NUMERO_PRENSA = "FALTA_COMPLETAR_NUMERO_PRENSA";
        private static string FALTA_COMPLETAR_APORTE = "FALTA_COMPLETAR_APORTE";
        private static string FALTA_COMPLETAR_NOMBRE_EXTERNO = "FALTA_COMPLETAR_NOMBRE_EXTERNO";
        private static string FALTA_SELECCIONAR_INTERNO = "FALTA_SELECCIONAR_INTERNO";
        private static string FALTA_SELECCIONAR_EXTERNO = "FALTA_SELECCIONAR_EXTERNO";

        private IRepository repo = new Repository();
        private UserControl proveniente;
        private PrensaOB prensaUO;
        private bool modificacion;
        private bool entradaPrensaNueva;
        private int orden;
        private string mensaje = "OK";

        #region INICIALIZADOR
        /// <summary>
        /// Inicializador para una prensa nueva.
        /// </summary>
        /// <param name="entradaPrensa"></param>
        /// <param name="entradaPrensaNueva"></param>
        public AgregarPrensa(EntradaPrensa entradaPrensa, bool entradaPrensaNueva)
        {
            InitializeComponent();
            this.proveniente = entradaPrensa;
            this.CargarInfo();
            this.modificacion = false;
            this.entradaPrensaNueva = entradaPrensaNueva;
        }

        /// <summary>
        /// Inicializador para modificar una prensa.
        /// </summary>
        /// <param name="entradaPrensa"></param>
        /// <param name="prensaUO"></param>
        /// <param name="orden"></param>
        /// <param name="entradaPrensaNueva"></param>
        public AgregarPrensa(EntradaPrensa entradaPrensa, PrensaOB prensaUO, int orden, bool entradaPrensaNueva)
        {
            InitializeComponent();
            this.proveniente = entradaPrensa;
            this.prensaUO = prensaUO;
            this.modificacion = true;
            this.entradaPrensaNueva = entradaPrensaNueva;
            this.orden = orden;
            this.CargarInfo(this.prensaUO);
        }
        #endregion

        private void CargarInfo(PrensaOB prensaUO = null)
        {
            TipoPasaje.ItemsSource = repo.ObtenerPrensaTipoPasaje();
            Internos.ItemsSource = repo.ObtenerInternos();
            Externos.ItemsSource = repo.ObtenerExternos();
            Actividades.ItemsSource = repo.ObtenerActividades();

            if (modificacion)
            {
                TipoPasaje.SelectedItem = prensaUO.tipoPasaje;
                Internos.SelectedItem = prensaUO.interno;

                if (prensaUO.comprador != null)
                {
                    if (prensaUO.comprador.GetType() == typeof(Interno))
                    {
                        TipoAportante.SelectedIndex = 0;
                        Internos.SelectedItem = prensaUO.comprador;
                    }
                    else
                    {
                        TipoAportante.SelectedIndex = 1;
                        Externos.SelectedItem = prensaUO.comprador;
                        ExternoDesconocido.IsChecked = false;
                        ExternoDesconocido_Unchecked(null, null);
                        ExternoExistente.IsChecked = true;
                        ExternoExistente_Check(null, null);
                    }
                }
                else
                {
                    TipoAportante.SelectedIndex = 1;
                    ExternoDesconocido.IsChecked = true;
                    ExternoDesconocido_Checked(null, null);
                }
                Aporte.Text = prensaUO.aporte.ToString();
                NumeroPrensa.Text = prensaUO.prensaNumero.ToString();
                Observacion.Text = prensaUO.observacion;
            }
            else
            {
                TipoPasaje.SelectedIndex = 0;
                TipoAportante.SelectedIndex = 0;
            }
        }

        private PrensaOB ObtenerPrensaCargada()
        {
            PrensaOB prensa = new PrensaOB();

            if (!entradaPrensaNueva)
                prensa.entradaPrensaId = ((EntradaPrensa)proveniente).entradaPrensaUO.id;

            if (modificacion)
                prensa.id = prensaUO.id;

            if (Actividades.SelectedIndex == 0 || Actividades.SelectedIndex == -1)
                prensa.actividadId = null;
            else
                prensa.actividadId = ((Actividad)Actividades.SelectedItem).id;
            prensa.aporte = Convert.ToInt32(Aporte.Text);
            prensa.interno = (Interno)Internos.SelectedItem;

            if (TipoAportante.SelectedIndex == 0)
                prensa.comprador = prensa.interno;
            else
            {
                if ((bool)ExternoDesconocido.IsChecked)
                    prensa.comprador = null;
                else if ((bool)ExternoExistente.IsChecked)
                    prensa.comprador = Externos.SelectedItem;
                else
                    prensa.comprador = repo.AgregarExterno(NombreExterno.Text, Observacion.Text, prensa.interno);
            }

            prensa.observacion = Observacion.Text;
            prensa.prensaNumero = Convert.ToInt32(NumeroPrensa.Text);
            prensa.tipoPasaje = (Tipo)TipoPasaje.SelectedItem;

            return prensa;
        }

        private bool ValidarDatos()
        {
            Validador validador = new Validador();

            if (TipoAportante.SelectedIndex == 1)
            {
                if (!(bool)ExternoDesconocido.IsChecked)
                {
                    if ((bool)ExternoExistente.IsChecked)
                        validador.Add(ExternoLabel, Externos, AgregarPrensa.FALTA_SELECCIONAR_INTERNO);
                    else
                        validador.Add(NombreExternoLabel, NombreExterno, AgregarPrensa.FALTA_COMPLETAR_NOMBRE_EXTERNO);
                }
            }

            validador.Add(InternosLabel, Internos, AgregarPrensa.FALTA_SELECCIONAR_INTERNO);
            validador.Add(AporteLabel, Aporte, AgregarPrensa.FALTA_COMPLETAR_APORTE);
            validador.Add(NumeroPrensaLabel, NumeroPrensa, AgregarPrensa.FALTA_COMPLETAR_NUMERO_PRENSA);

            validador.Validar();
            //TODO: mostrar mensaje de error o correcto.

            foreach (object[] objecto in validador.errores)
            {
                foreach (UIElement ele in ((DockPanel)objecto[0]).Children)
                {
                    if (ele.Uid == "contenedorMensaje")
                    {
                        foreach (UIElement ele2 in ((StackPanel)ele).Children)
                        {
                            if (ele2.Uid == "mensaje")
                            {
                                ele2.Visibility = Visibility.Visible;
                                ((Label)ele2).Content = ((string)objecto[1]);
                            }
                        }
                    }
                }
            }

            return validador.errores.Count == 0;
        }

        private void TipoAportante_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TipoAportante.SelectedIndex)
            {
                case 0:
                    NombreExterno.Text = "";
                    ObservacionExterno.Text = "";
                    NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                    ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                    ExternoExistenteLabel.Visibility = System.Windows.Visibility.Collapsed;
                    ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
                    ExternoDesconocidoLabel.Visibility = System.Windows.Visibility.Collapsed;
                    ExternoDesconocido.IsChecked = false;
                    break;
                case 1:
                    ExternoDesconocidoLabel.Visibility = System.Windows.Visibility.Visible;
                    ExternoDesconocido.IsChecked = true;
                    ExternoDesconocido_Checked(null, null);
                    break;
            }
        }

        #region CHECKS
        private void ExternoDesconocido_Checked(object sender, RoutedEventArgs e)
        {
            NombreExterno.Text = "";
            ObservacionExterno.Text = "";
            NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            ExternoExistenteLabel.Visibility = System.Windows.Visibility.Collapsed;
            ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ExternoDesconocido_Unchecked(object sender, RoutedEventArgs e)
        {
            ExternoExistenteLabel.Visibility = System.Windows.Visibility.Visible;
            ExternoExistente.IsChecked = true;
            ExternoExistente_Check(null, null);
        }

        private void ExternoExistente_Check(object sender, RoutedEventArgs e)
        {
            NombreExternoLabel.Visibility = System.Windows.Visibility.Collapsed;
            ObservacionExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

            ExternoLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void ExternoExistente_Uncheck(object sender, RoutedEventArgs e)
        {
            ExternoLabel.Visibility = System.Windows.Visibility.Collapsed;

            NombreExternoLabel.Visibility = System.Windows.Visibility.Visible;
            ObservacionExternoLabel.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion

        #region CLICKS
        private void AgregarActividad_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new AgregarActividad(AgregarActividad.AGREGAR_PRENSA, this);
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = proveniente;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarDatos())
            {
                PrensaOB prensa = ObtenerPrensaCargada();
                prensa.modificado = true;

                if (!modificacion)
                    ((EntradaPrensa)this.proveniente).entradaPrensaUO.prensas.Add(prensa);
                else
                {
                    ((EntradaPrensa)this.proveniente).entradaPrensaUO.prensas.RemoveAt(orden);
                    ((EntradaPrensa)this.proveniente).entradaPrensaUO.prensas.Add(prensa);
                    ((EntradaPrensa)this.proveniente).entradaPrensaUO.prensas.Move(((EntradaPrensa)this.proveniente).entradaPrensaUO.prensas.Count - 1, orden);
                }
                MainWindow.self.Content = proveniente;
            }
        }
        #endregion
    }
}
