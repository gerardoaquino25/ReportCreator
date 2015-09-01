using ReportCreator.Entities;
using ReportCreator.Entities.UtilityObject;
using ReportCreator.Model;
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
    /// Lógica de interacción para Prensa.xaml
    /// </summary>
    public partial class EntradaPrensa : UserControl
    {
        private IRepository repo = new Repository();

        private bool entradaPrensaNueva;
        private bool informeNuevo;
        public EntradaPrensaUO entradaPrensaUO;

        #region INICIALIZADOR
        /// <summary>
        /// Inicializador para una entrada nueva.
        /// </summary>
        /// <param name="informeId"></param>
        /// <param name="titulo"></param>
        /// <param name="entradaPrensaNueva"></param>
        /// <param name="informeNuevo"></param>
        public EntradaPrensa(long informeId, string titulo, bool entradaPrensaNueva, bool informeNuevo)
        {
            InitializeComponent();

            this.entradaPrensaUO = new EntradaPrensaUO();
            this.entradaPrensaUO.titulo = titulo;
            this.entradaPrensaUO.informeId = informeId;

            this.informeNuevo = informeNuevo;
            this.entradaPrensaNueva = entradaPrensaNueva;

            this.CargarInfo();
        }

        /// <summary>
        /// Inicializador para una entrada ya creada, o sea para la modificación de la misma.
        /// </summary>
        /// <param name="idEntrada"></param>
        /// <param name="informeNuevo"></param>
        public EntradaPrensa(long idEntrada, bool informeNuevo)
        {
            InitializeComponent();

            this.entradaPrensaUO = repo.ObtenerEntradaPrensa(idEntrada);

            this.informeNuevo = informeNuevo;
            this.entradaPrensaNueva = false;

            this.CargarInfo();
        }
        #endregion

        private void CargarInfo()
        {
            if (!entradaPrensaNueva)
                entradaPrensaUO.prensas = repo.ObtenerPrensasByEntradaId((long)this.entradaPrensaUO.id);

            entradaPrensaUO.suscripciones = repo.ObtenerSuscripciones();

            Titulo.DataContext = entradaPrensaUO;
            Prensas.ItemsSource = entradaPrensaUO.prensas;
            Suscripciones.DataContext = entradaPrensaUO;
        }

        #region KEY DOWN
        private void RowPrensa_KeyDown(object sender, KeyEventArgs e)
        {
            //if (Key.Delete == e.Key)
            //{
            //    DataGridRow row = sender as DataGridRow;
            //    PadronCF entrada = (PadronCF)row.Item;

            //    if (entrada.entradaCampaniaFinancieraId == entradaCampaniaFinanciera.id)
            //    {
            //        entradaCampaniaFinanciera.padrones.Remove(entrada);
            //        //repo.BorrarEntrada(entrada.id, entrada.tipo.id);
            //    }
            //}
        }
        #endregion

        #region CLICKS
        private void RowPrensa_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            PrensaOB entrada = (PrensaOB)row.Item;
            MainWindow.SetContent(new AgregarPrensa(this, entrada, row.GetIndex(), entrada.entradaPrensaId == null));
        }

        private void AgregarPasajePrensa_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new AgregarPrensa(this, this.entradaPrensaUO.id == null));
        }

        private void AgregarSuscripcion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            if (this.entradaPrensaUO.informeId == null && (Prensas.Items.Count > 0 || Suscripciones.Items.Count > 0))
            {
                //TODO: Mensaje de aviso
            }

            MainWindow.SetContent(new Borrador((long)entradaPrensaUO.informeId, informeNuevo));
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (this.entradaPrensaUO.id == null)
                repo.AgregarEntradaPrensa(this.entradaPrensaUO);
            else
                repo.GuardarEntradaPrensa(this.entradaPrensaUO);
            MainWindow.SetContent(new Borrador((long)this.entradaPrensaUO.informeId, informeNuevo));
        }
        #endregion
    }
}
