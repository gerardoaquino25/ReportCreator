using ReportCreator.Entities.Authentication;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void CrearNuevoUsuarioClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SetContent(new CrearNuevoUsuario(), true);
        }

        private void RecuperarContraseniaClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SetContent(new RecuperarContrasenia(Username.Text), true);
        }

        private void LabelMouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void LabelMouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}
