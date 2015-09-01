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
        public static IList<UserControl> ElementsToAdd;
        delegate void MyFunctionDelegate(string a);

        public MainWindow()
        {
            InitializeComponent();
            ElementsToAdd = new List<UserControl>();
            ContenidoReference = Contenido;
            InitBarReference = InitBar;
            ContenidoReference.Children.Add(new LoginWindow(viewModel));
        }

        public static void SetContent(UIElement elemento)
        {
            ContenidoReference.Children.RemoveAt(0);

            for (int i = InitBarReference.Children.Count - 1; i >= 0; i--)
                InitBarReference.Children.RemoveAt(i);

            for (int i = ElementsToAdd.Count - 1; i >= 0; i--)
            {
                InitBarReference.Children.Add(ElementsToAdd[i]);
                ElementsToAdd.RemoveAt(i);
            }

            ContenidoReference.Children.Add(elemento);
        }

        public static void AddButtonToInitBar(UserControl elemento)
        {
            ElementsToAdd.Add(elemento);
        }

        public static void AddButtonNowToInitBar(UserControl elemento, bool removeAll = false)
        {
            if (removeAll)
            {
                for (int i = InitBarReference.Children.Count - 1; i >= 0; i--)
                    InitBarReference.Children.RemoveAt(i);
            }

            ContenidoReference.Children.Add(elemento);
        }
    }
}
