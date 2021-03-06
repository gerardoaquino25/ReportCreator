﻿using ReportCreator.Entities;
using ReportCreator.Model;
using ReportCreator.View.UtilityElement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para Borradores.xaml
    /// </summary>
    public partial class Borradores : UserControl
    {
        ObservableCollection<Informe> informes;
        IRepository repo = new Repository();

        public Borradores()
        {
            InitializeComponent();

            VolverButtonUE volverButton = new VolverButtonUE();
            volverButton.Name = "Volver";
            volverButton.Visibility = Visibility.Visible;
            volverButton.MouseLeftButtonUp += Volver_Click;
            MainWindow.AddButtonToInitBar(volverButton);

            informes = new ObservableCollection<Informe> (repo.ObtenerInformesBorrador());
            Informes.ItemsSource = informes;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            MainWindow.SetContent(new Borrador(((Informe)row.Item).id, false));
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new EnvioInforme());
        }

        private void Row_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Delete == e.Key)
            {
                DataGridRow row = sender as DataGridRow;
                Informe informe = ((Informe)row.Item);
                repo.BorrarInforme(informe.id);
                informes.Remove(informe);
            }
        }
    }
}
