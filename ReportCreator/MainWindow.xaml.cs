using ReportCreator.Entities.Authentication;
using ReportCreator.View;
using ReportCreator.View.Authentication;
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

namespace ReportCreator
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());
        public static Grid ContenidoReference;
        public static DockPanel InitBarReference;
        public static DockPanel VolverReference;

        public MainWindow()
        {
            InitializeComponent();
            ContenidoReference = Contenido;
            InitBarReference = InitBar;
            InitBarReference.Visibility = Visibility.Collapsed;
            ContenidoReference.Children.Add(new LoginWindow(viewModel));
            VolverReference = Volver;
        }

        public static void SetContent(UIElement elemento, bool showBar = false)
        {
            ContenidoReference.Children.RemoveAt(0);
            if (showBar)
                InitBarReference.Visibility = Visibility.Visible;
            else
                InitBarReference.Visibility = Visibility.Collapsed;
            ContenidoReference.Children.Add(elemento);
        }

        private void Volver_MouseEnter(object sender, MouseEventArgs e)
        {
            Volver.Cursor = Cursors.Hand;
        }

        private void Volver_MouseLeave(object sender, MouseEventArgs e)
        {
            Volver.Cursor = Cursors.Arrow;
        }
    }
}
