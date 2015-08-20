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
        public static Window self;
        //public AuthenticationViewModel viewModel;
        public static AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());

        public MainWindow()
        {
            InitializeComponent();
            self = this;

            this.Content = new LoginWindow(viewModel);
        }
    }
}
