using ReportCreator.Entities;
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
    /// Lógica de interacción para AgregarPrensa.xaml
    /// </summary>
    public partial class AgregarPrensa : UserControl
    {
        public const string AGREGAR_PRENSA = "AGREGAR_PRENSA";
        public const string ABM_ACTIVIDAD = "ABM_ACTIVIDAD";

        private IRepository repo = new Repository();
        private UserControl proveniente;
        private PrensaUO prensaUO;
        private bool modificacion;
        private bool entradaPrensaNueva;
        private int orden;

        public AgregarPrensa(EntradaPrensa entradaPrensa, bool entradaPrensaNueva)
        {
            InitializeComponent();
            this.proveniente = entradaPrensa;
            this.Iniciar();
            this.modificacion = false;
            this.entradaPrensaNueva = entradaPrensaNueva;
        }

        public AgregarPrensa(EntradaPrensa entradaPrensa, PrensaUO prensaUO, int orden, bool entradaPrensaNueva)
        {
            InitializeComponent();
            this.proveniente = entradaPrensa;
            this.prensaUO = prensaUO;
            this.modificacion = true;
            this.entradaPrensaNueva = entradaPrensaNueva;
            this.orden = orden;
            this.Iniciar(this.prensaUO);
        }

        private void Iniciar(PrensaUO prensaUO = null)
        {
            TipoPasaje.ItemsSource = repo.ObtenerPrensaTipoPasaje();
            Internos.ItemsSource = repo.ObtenerInternos();
            Externos.ItemsSource = repo.ObtenerExternos();
            Actividades.ItemsSource = repo.ObtenerActividades();

            if (modificacion)
            {
                TipoPasaje.SelectedItem = prensaUO.tipoPasaje;

                if (prensaUO.comprador != null)
                {
                    if (prensaUO.comprador.GetType() == typeof(Interno))
                    {
                        TipoAportante.SelectedIndex = 0;
                        Internos.SelectedItem = prensaUO.comprador;
                    }
                    else
                    {
                        TipoAportante.SelectedIndex = 2;

                        if (((Externo)prensaUO.comprador).id != null)
                        {
                            Externos.SelectedItem = prensaUO.comprador;
                            ExternoExistente.IsChecked = true;
                            ExternoExistenteCheck(null, null);
                        }
                        else
                        {
                            ExternoDesconocido.IsChecked = true;
                        }

                    }
                }
                else
                {
                    TipoAportante.SelectedItem = 2;
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

        private void AgregarActividadClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new AgregarActividad(AgregarActividad.AGREGAR_PRENSA, this);
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = proveniente;
        }

        private PrensaUO CargarPrensa()
        {
            PrensaUO prensa = new PrensaUO();
            if (!entradaPrensaNueva)
                prensa.entradaPrensaId = ((EntradaPrensa)proveniente).entradaPrensaUO.id;

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

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            PrensaUO prensa = CargarPrensa();
            prensa.modificado = true;

            if (!modificacion)
            {
                ((EntradaPrensa)this.proveniente).entradaPrensaUO.prensas.Add(prensa);
                MainWindow.self.Content = proveniente;
            }
            else
            {
                ((EntradaPrensa)this.proveniente).entradaPrensaUO.prensas[orden] = prensa;
            }
        }

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
            ExternoExistenteCheck(null, null);
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
    }
}
