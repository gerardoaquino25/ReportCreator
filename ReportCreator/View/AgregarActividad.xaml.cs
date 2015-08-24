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
    /// Lógica de interacción para AgregarActividad.xaml
    /// </summary>
    public partial class AgregarActividad : UserControl
    {
        public const string AGREGAR_PRENSA = "AGREGAR_PRENSA";
        public const string ABM_ACTIVIDAD = "ABM_ACTIVIDAD";
        IRepository repo = new Repository();

        public AgregarActividad()
        {
            InitializeComponent();
        }

        public AgregarActividad(string proveniente, object[] datos)
        {
            InitializeComponent();

            switch(proveniente){
                case AGREGAR_PRENSA:
                    break;
                case ABM_ACTIVIDAD:
                    break;
            }
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {
            Actividad actividad = new Actividad();
            actividad.nombre = Nombre.Text;
            actividad.detalle = Detalle.Text;
            actividad.fecha = (DateTime)Fecha.SelectedDate;

            repo.AgregarActividad(actividad);
        }

        private void VolverClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
