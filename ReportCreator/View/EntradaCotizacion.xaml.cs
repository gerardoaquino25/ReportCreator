using ReportCreator.Entities;
using ReportCreator.Entities.UtilityObject;
using ReportCreator.Model;
using ReportCreator.View.UtilityElement;
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
    /// Lógica de interacción para Cotizacion.xaml
    /// </summary>
    public partial class EntradaCotizacion : UserControl
    {
        private IRepository repo = new Repository();
        private EntradaCotizacionUO cotizacion;
        private bool nuevo;

        public EntradaCotizacion()
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

        public EntradaCotizacion(long idEntrada, bool nuevo)
            : this()
        {
            this.cotizacion = repo.ObtenerEntradaCotizacion(idEntrada);
            iniciar();
            this.nuevo = nuevo;
        }

        public EntradaCotizacion(long idInforme, string asunto, bool nuevo)
            : this()
        {
            InitializeComponent();
            iniciar();
            cotizacion.informeId = idInforme;
            cotizacion.titulo = asunto;
            this.nuevo = nuevo;
        }

        public void iniciar()
        {
            int minimo = DateTime.Now.Year - 10;
            int maximo = DateTime.Now.Year + 10;

            IList<int> anios = new List<int>();
            for (int i = minimo; i <= maximo; i++)
                anios.Add(i);

            Anio.ItemsSource = anios;
            Internos.ItemsSource = repo.ObtenerInternos();

            if (cotizacion == null)
            {
                cotizacion = new EntradaCotizacionUO();
                cotizacion.cotizacionesInternos = new List<CotizacionInterno>();
                cotizacion.anio = (int)Anio.Items[10];
                cotizacion.mes = DateTime.UtcNow.Month;
            }

            Anio.SelectedItem = cotizacion.anio;
            Mes.SelectedIndex = cotizacion.mes - 1;
            CotizacionesDG.DataContext = cotizacion.cotizacionesInternos;
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new Borrador((long)cotizacion.informeId, nuevo));
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (cotizacion.id == 0)
                cotizacion.id = repo.AgregarEntrada((long)cotizacion.informeId, cotizacion.titulo, 9);
            cotizacion.mes = Mes.SelectedIndex + 1;
            cotizacion.anio = (int)Anio.SelectedItem;
            repo.GuardarEntradaCotizacion(cotizacion);

            MainWindow.SetContent(new Borrador((long)cotizacion.informeId, nuevo));
        }
    }
}
