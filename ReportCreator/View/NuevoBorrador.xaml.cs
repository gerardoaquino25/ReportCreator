using ReportCreator.Entities;
using ReportCreator.Model;
using ReportCreator.View;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para NuevoBorrador.xaml
    /// </summary>
    public partial class NuevoBorrador : UserControl
    {
        Informe informe;
        long idInforme = 0;
        IRepository repo = new Repository();

        public NuevoBorrador()
        {
            InitializeComponent();
        }

        public NuevoBorrador(long idInforme)
        {
            InitializeComponent();
            informe = repo.ObtenerInforme(idInforme);
            this.idInforme = idInforme;
            Entradas.ItemsSource = informe.entradas;
            Asunto.Text = informe.asunto;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (idInforme == 0)
                idInforme = repo.AgregarInforme(Asunto.Text);
            else
                repo.GuardarInforme(idInforme, Asunto.Text);

            MainWindow.self.Content = new NuevaEntrada(idInforme);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (idInforme == 0)
                repo.AgregarInforme("");
            else
                repo.GuardarInforme(idInforme, Asunto.Text);
            MainWindow.self.Content = new EnvioInforme();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            Entrada entrada = (Entrada)row.Item;

            switch (entrada.tipo.id)
            {
                case 1:
                    MainWindow.self.Content = new EntradaGenerica(entrada.id);
                    break;
                case 2:
                    //MainWindow.self.Content = new NuevoBorrador();
                    break;
                case 3:
                    //MainWindow.self.Content = new NuevoBorrador();
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
                    //MainWindow.self.Content = new NuevoBorrador();
                    break;
                case 9:
                    MainWindow.self.Content = new EntradaCotizacion(entrada.id);
                    break;
                case 10:
                    //MainWindow.self.Content = new NuevoBorrador();
                    break;
                case 11:
                    //MainWindow.self.Content = new NuevoBorrador();
                    break;
            }


            long slsa = 0;
        }
    }
}
