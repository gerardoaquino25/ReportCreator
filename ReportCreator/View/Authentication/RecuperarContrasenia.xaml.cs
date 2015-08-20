﻿using ReportCreator.Entities;
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

namespace ReportCreator.View.Authentication
{
    /// <summary>
    /// Lógica de interacción para RecuperarContrasenia.xaml
    /// </summary>
    public partial class RecuperarContrasenia : UserControl
    {
        IRepository repo = new Repository();

        public RecuperarContrasenia(string username)
        {
            InitializeComponent();
            Username.Text = username;
        }

        private void VolverClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new LoginWindow(MainWindow.viewModel);
        }

        private void RecuperarClick(object sender, RoutedEventArgs e)
        {
            Notificacion resultado = repo.RecuperarContraseña(Username.Text);
            if (resultado.Detalle == Notificacion.EMAIL_ENVIADO)
            {
                MainWindow.self.Content = new LoginWindow(MainWindow.viewModel);
            }
            else if (resultado.Detalle == Notificacion.CAMBIO_CONTRASENIA_KO)
            {

            }
            else if (resultado.Detalle == Notificacion.EMAIL_ENVIADO_KO)
            {

            }
        }
    }
}
