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
    /// Lógica de interacción para MailReceivers.xaml
    /// </summary>
    public partial class MailReceivers : UserControl
    {
        IList<MailReceiver> mailReceivers;
        IRepository repo = new Repository();

        public MailReceivers()
        {
            InitializeComponent();
            mailReceivers = repo.ObtenerMailReceivers();
            MailReceiversDG.ItemsSource = mailReceivers;
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {
            repo.GuardarMailReceivers((List<MailReceiver>)MailReceiversDG.ItemsSource);
            MainWindow.self.Content = new Opciones();
        }

        private void VolverClick(object sender, RoutedEventArgs e)
        {
            MainWindow.self.Content = new Opciones();
        }
    }
}
