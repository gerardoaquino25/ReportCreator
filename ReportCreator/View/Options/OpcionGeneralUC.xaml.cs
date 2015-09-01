using ReportCreator.Entities;
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

namespace ReportCreator.View.Options
{
    /// <summary>
    /// Lógica de interacción para OpcionGeneral.xaml
    /// </summary>
    public partial class OpcionGeneralUC : UserControl
    {
        IRepository repo = new Repository();

        public OpcionGeneralUC()
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

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new Opciones());
        }

        private void ContraseniaCollapsed(object sender, RoutedEventArgs e)
        {
            ContraseniaActual.Password = "";
            ContraseniaNueva.Password = "";
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            IList<OpcionGeneral> listaOpcionesGenerales = new List<OpcionGeneral>();

            if (!String.IsNullOrEmpty(AsuntoDefault.Text))
            {
                OpcionGeneral asuntoDefault = new OpcionGeneral();
                asuntoDefault.cambio = true;
                asuntoDefault.nombre = OpcionGeneral.ASUNTO_DEFAULT;
                asuntoDefault.valores.Add(new KeyValuePair<string, object>(OpcionGeneral.VALUE, AsuntoDefault.Text));
                listaOpcionesGenerales.Add(asuntoDefault);
            }

            if (CambiarContrasenia.IsExpanded && !String.IsNullOrEmpty(ContraseniaActual.Password) && !String.IsNullOrEmpty(ContraseniaNueva.Password))
            {
                OpcionGeneral cambioContrasenia = new OpcionGeneral();
                cambioContrasenia.cambio = true;
                cambioContrasenia.nombre = OpcionGeneral.CAMBIO_CONTRASENIA;
                cambioContrasenia.valores.Add(new KeyValuePair<string, object>(OpcionGeneral.CONTRASENIA_ACTUAL, ContraseniaActual.Password));
                cambioContrasenia.valores.Add(new KeyValuePair<string, object>(OpcionGeneral.CONTRASENIA_NUEVA, ContraseniaNueva.Password));
                listaOpcionesGenerales.Add(cambioContrasenia);
            }

            repo.GuardarOpcionesGenerales(listaOpcionesGenerales);
        }
    }
}
