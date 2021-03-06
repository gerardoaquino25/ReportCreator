﻿using ReportCreator.Entities;
using ReportCreator.Entities.UtilityObject;
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

namespace ReportCreator.View
{
    /// <summary>
    /// Lógica de interacción para AgregarActividad.xaml
    /// </summary>
    public partial class AgregarActividad : UserControl
    {
        public const string AGREGAR_PRENSA = "AGREGAR_PRENSA";
        public const string ABM_ACTIVIDAD = "ABM_ACTIVIDAD";

        public string proveniente;
        public UserControl previous;
        private IRepository repo = new Repository();

        public AgregarActividad(string proveniente, AgregarPrensa previous)
        {
            InitializeComponent();

            GuardarButtonUE guardarButton = new GuardarButtonUE();
            guardarButton.Name = "Guardar";
            guardarButton.Visibility = Visibility.Visible;
            guardarButton.MouseLeftButtonUp += Guardar_Click;
            MainWindow.AddButtonToInitBar(guardarButton);

            VolverButtonUE volverButton = new VolverButtonUE();
            volverButton.Name = "Volver";
            volverButton.Visibility = Visibility.Visible;
            volverButton.MouseLeftButtonUp += Volver_Click;
            MainWindow.AddButtonToInitBar(volverButton);

            this.proveniente = proveniente;
            this.previous = previous;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Actividad actividad = new Actividad();
            actividad.nombre = Nombre.Text;
            actividad.detalle = Detalle.Text;
            actividad.fecha = (DateTime)Fecha.SelectedDate;
            repo.AgregarActividad(actividad);

            this.VolverAOrigen();
        }

        private void VolverAOrigen()
        {
            switch (this.proveniente)
            {
                case AGREGAR_PRENSA:
                    ((AgregarPrensa)previous).Actividades.ItemsSource = repo.ObtenerActividades();
                    MainWindow.SetContent(previous);
                    break;
                case ABM_ACTIVIDAD:
                    break;
            }
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            this.VolverAOrigen();
        }
    }
}
