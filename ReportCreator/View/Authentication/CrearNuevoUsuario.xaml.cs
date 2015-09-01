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

namespace ReportCreator.View.Authentication
{
    /// <summary>
    /// Lógica de interacción para CrearNuevoUsuario.xaml
    /// </summary>
    public partial class CrearNuevoUsuario : UserControl
    {
        IRepository repo = new Repository();

        public CrearNuevoUsuario()
        {
            InitializeComponent();
        }

        private void Crear_Click(object sender, RoutedEventArgs e)
        {
            Notificacion respuesta = repo.AgregarUsuario(Username.Text, Email.Text, Password.Password);

            if (respuesta.Detalle.Equals(Notificacion.USUARIO_CREADO))
            {
                MainWindow.SetContent(new LoginWindow(MainWindow.viewModel), true);
            }
            else
            {
                if (respuesta.Detalle.Equals(Notificacion.USUARIO_CREADO_KO))
                {

                }

                if (respuesta.Detalle.Equals(Notificacion.USUARIO_YA_EXISTENTE))
                {

                }
            }
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new LoginWindow(MainWindow.viewModel), true);
        }
    }
}
