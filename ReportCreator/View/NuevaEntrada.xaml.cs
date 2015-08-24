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
    /// Lógica de interacción para NuevaEntrada.xaml
    /// </summary>
    public partial class NuevaEntrada : UserControl
    {
        int seleccionado = 0;
        long idInforme;
        private bool nuevo;
        IRepository repo = new Repository();

        public NuevaEntrada(bool nuevo)
        {
            InitializeComponent();
            this.nuevo = nuevo;
        }

        public NuevaEntrada(long idInforme, bool nuevo)
        {
            InitializeComponent();
            this.idInforme = idInforme;
            this.nuevo = nuevo;
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            seleccionado = ComboBoxTipoEntrada.SelectedIndex;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            long idEntrada = 0;
            switch (seleccionado)
            {
                //case 0:
                //    break;
                case 1:
                    MainWindow.self.Content = new EntradaGenerica(idInforme, asunto.Text, nuevo);
                    break;
                case 2:
                    //MainWindow.self.Content = new NuevoBorrador();
                    break;
                case 3:
                    idEntrada = repo.AgregarEntrada(idInforme, asunto.Text, 3);
                    MainWindow.self.Content = new EntradaCampaniaFinanciera(idEntrada, nuevo, true);
                    break;
                case 4:
                    //MainWindow.self.Content = new NuevoBorrador();
                    break;
                case 5:
                    //MainWindow.self.Content = new NuevoBorrador();
                    break;
                case 6:
                    //MainWindow.self.Content = new NuevoBorrador();
                    break;
                case 7:
                    //MainWindow.self.Content = new NuevoBorrador();
                    break;
                case 8:
                    idEntrada = repo.AgregarEntrada(idInforme, asunto.Text, 8);
                    MainWindow.self.Content = new Prensa(idInforme, asunto.Text, nuevo);
                    break;
                case 9:
                    MainWindow.self.Content = new EntradaCotizacion(idInforme, asunto.Text, nuevo);
                    break;
                case 10:
                    //MainWindow.self.Content = new NuevoBorrador();
                    break;
                case 11:
                    //MainWindow.self.Content = new NuevoBorrador();
                    break;
            };
        }
    }
}
