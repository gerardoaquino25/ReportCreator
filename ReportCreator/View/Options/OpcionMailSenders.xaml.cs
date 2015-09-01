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
    /// Lógica de interacción para Mail.xaml
    /// </summary>
    public partial class OpcionMailSenders : UserControl
    {
        IRepository repo = new Repository();

        public OpcionMailSenders()
        {
            InitializeComponent();

            AgregarButtonUE agregarButton = new AgregarButtonUE();
            agregarButton.Name = "Agregar";
            agregarButton.Visibility = Visibility.Visible;
            agregarButton.MouseLeftButtonUp += Agregar_Click;
            MainWindow.AddButtonToInitBar(agregarButton);

            VolverButtonUE volverButton = new VolverButtonUE();
            volverButton.Name = "Volver";
            volverButton.Visibility = Visibility.Visible;
            volverButton.MouseLeftButtonUp += Volver_Click;
            MainWindow.AddButtonToInitBar(volverButton);

            MailSendersDG.ItemsSource = repo.ObtenerMailSenders();
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new DetalleMail());
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new Opciones());
        }

        private void Row_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Delete == e.Key)
            {
                DataGridRow row = sender as DataGridRow;
                repo.BorrarMailSender(((MailSender)row.Item).id);
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            MainWindow.SetContent(new DetalleMail(((MailSender)row.Item).id));
        }
    }
}
