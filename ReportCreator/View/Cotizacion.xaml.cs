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
    /// Lógica de interacción para Cotizacion.xaml
    /// </summary>
    public partial class Cotizacion : UserControl
    {
        private long idInforme;
        private string asunto;
        private IRepository repo = new Repository();
        private ReportCreator.Entities.Cotizacion cotizacion;
        private List<CotizacionInterno> cotizacionesInternos;

        public Cotizacion()
        {
            InitializeComponent();
            iniciar();
            Interno.ItemsSource = repo.GetInternos();
            cotizacion = new ReportCreator.Entities.Cotizacion();
            cotizacionesInternos = new List<CotizacionInterno>();
        }

        public Cotizacion(long idInforme, string asunto)
        {
            InitializeComponent();
            iniciar();
            Interno.ItemsSource = repo.GetInternos();
            this.idInforme = idInforme;
            this.asunto = asunto;
            cotizacion = new ReportCreator.Entities.Cotizacion();


            CotizacionInterno cotizacionNueva = new CotizacionInterno();
            cotizacionNueva.interno = new Interno() { id = 0, circulo = "Hola", nombre = "sdankld", activo = true};
            cotizacionNueva.fecha = DateTime.UtcNow;
            cotizacionNueva.observacion = "observacion asdkd";

            cotizacionesInternos = new List<CotizacionInterno>();
            cotizacionesInternos.Add(cotizacionNueva);
            CotizacionesDG.ItemsSource = cotizacionesInternos;
        }

        public void iniciar()
        {
            int minimo = DateTime.Now.Year - 10;
            int maximo = DateTime.Now.Year + 10;

            IList<int> anios = new List<int>();
            for (int i = minimo; i <= maximo; i++)
                anios.Add(i);

            Anio.ItemsSource = anios;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CotizacionInterno cotizacionNueva = new CotizacionInterno();
            cotizacionNueva.interno = (Interno)Interno.SelectedItem;
            cotizacionNueva.fecha = FechaIngreso.SelectedDate != null ? (DateTime)FechaIngreso.SelectedDate : DateTime.UtcNow;
            cotizacionNueva.observacion = Observacion.Text;
            cotizacionesInternos.Add(cotizacionNueva);
            //CotizacionesDG.ItemsSource = cotizacion.cotizacionesInternos;
            //CotizacionesDG.ItemsSource = null;
            //CotizacionesDG.ItemsSource = cotizacion.cotizacionesInternos;
        }
    }
}
