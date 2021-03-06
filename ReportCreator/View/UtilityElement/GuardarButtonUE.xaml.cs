﻿using System;
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

namespace ReportCreator.View.UtilityElement
{
    /// <summary>
    /// Lógica de interacción para GuardarButtonUE.xaml
    /// </summary>
    public partial class GuardarButtonUE : UserControl
    {
        public GuardarButtonUE()
        {
            InitializeComponent();
        }

        private void Guardar_MouseEnter(object sender, MouseEventArgs e)
        {
            Contenedor.Cursor = Cursors.Hand;
        }

        private void Guardar_MouseLeave(object sender, MouseEventArgs e)
        {
            Contenedor.Cursor = Cursors.Arrow;
        }
    }
}
