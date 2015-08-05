﻿using ReportCreator.Model;
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
        long idEntrada;

        public NuevaEntrada()
        {
            InitializeComponent();
        }

        public NuevaEntrada(long idInforme)
        {
            this.idInforme = idInforme;
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            seleccionado = ComboBoxTipoEntrada.SelectedIndex;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            switch (seleccionado)
            {
                case 0:
                    break;
                case 1:
                    IRepository repo = new Repository();
                    idEntrada = repo.AgregarEntrada(idInforme, asunto.Text, seleccionado);
                    MainWindow.self.Content = new EntradaGenerica(idInforme, idEntrada);
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
                    //MainWindow.self.Content = new NuevoBorrador();
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