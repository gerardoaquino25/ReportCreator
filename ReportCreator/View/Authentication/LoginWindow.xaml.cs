using ReportCreator.Entities.Authentication;
using System.Windows;
using System.Windows.Controls;

namespace ReportCreator.View.Authentication
{
    public interface IView
    {
        IViewModel ViewModel
        {
            get;
            set;
        }

        void Show();
    }

    public partial class LoginWindow : UserControl
    {
        public LoginWindow(AuthenticationViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        #region IView Members
        public IViewModel ViewModel
        {
            get { return DataContext as IViewModel; }
            set { DataContext = value; }
        }
        #endregion

        private void CrearNuevoUsuarioClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow.self.Content = new CrearNuevoUsuario();
        }

        private void RecuperarContraseniaClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow.self.Content = new RecuperarContrasenia(Username.Text);
        }
    }
}
