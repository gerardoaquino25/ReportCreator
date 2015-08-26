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
        private UserControl previous;
        public EntradaPrensaUO entradaPrensaUO;
        private PrensaUO prensaUO = null;

        public EntradaPrensa(long informeId, string titulo, bool entradaPrensaNueva, bool informeNuevo)
        {
            InitializeComponent();

            this.entradaPrensaUO = new EntradaPrensaUO();
            this.entradaPrensaUO.titulo = titulo;
            this.entradaPrensaUO.informeId = informeId;

            this.informeNuevo = informeNuevo;
            this.entradaPrensaNueva = entradaPrensaNueva;

            this.Iniciar();
        }

        public EntradaPrensa(long informeId, long entradaId, string titulo, bool informeNuevo)
        {
            InitializeComponent();

            this.entradaPrensaUO = new EntradaPrensaUO();
            this.entradaPrensaUO.titulo = titulo;
            this.entradaPrensaUO.informeId = informeId;
            this.entradaPrensaUO.id = entradaId;

            this.informeNuevo = informeNuevo;
            this.entradaPrensaNueva = false;

            this.Iniciar();
        }

        public EntradaPrensa(long idEntrada, bool informeNuevo)
        {
            InitializeComponent();

            this.entradaPrensaUO = repo.ObtenerEntradaPrensa(idEntrada);

            this.informeNuevo = informeNuevo;
            this.entradaPrensaNueva = false;

            this.Iniciar();
        }

        private void Iniciar()
        {
            if (!entradaPrensaNueva)
            {
                entradaPrensaUO.prensas = repo.ObtenerPrensasByEntradaId((long)this.entradaPrensaUO.id);
            }

            entradaPrensaUO.suscripciones = repo.ObtenerSuscripciones();

            Titulo.DataContext = entradaPrensaUO;
            Prensas.ItemsSource = entradaPrensaUO.prensas;
            Suscripciones.DataContext = entradaPrensaUO;
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {
            if (this.entradaPrensaUO.id == null)
                repo.AgregarEntradaPrensa(this.entradaPrensaUO);
            else
            {
            }
        }

        private void CancelarClick(object sender, RoutedEventArgs e)
        {
            if (this.entradaPrensaUO.informeId == null && (Prensas.Items.Count > 0 || Suscripciones.Items.Count > 0))
            {
                //TODO: Mensaje de aviso
            }

            MainWindow.self.Content = new Borrador((long)entradaPrensaUO.informeId, informeNuevo);
        }

        private void AgregarPasajePrensa_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new AgregarPrensa(this, this.entradaPrensaUO.id == null);
        }

        private void AgregarSuscripcion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RowPrensa_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            PrensaUO entrada = (PrensaUO)row.Item;
            MainWindow.self.Content = new AgregarPrensa(this, entrada, row.GetIndex(), entrada.entradaPrensaId == null);
        }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrensaUO prensa = new PrensaUO();
            prensa.actividadId = 1;
            prensa.aporte = 10;
            prensa.comprador = null;
            prensa.id = 1;
            prensa.interno = new Interno() { nombre = "Gerardo", circulo = "UTN" };
            prensa.modificado = false;
            prensa.observacion = "Hola";
            prensa.prensaNumero = 1905;
            prensa.tipoPasaje = new Tipo(1, "En mano");

            this.entradaPrensaUO.prensas.Add(prensa);
        }
    }
}
