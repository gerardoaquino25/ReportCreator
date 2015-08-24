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
    /// Lógica de interacción para Prensa.xaml
    /// </summary>
    public partial class Prensa : UserControl
    {
        private long idInforme;
        private string asunto;
        private bool nuevo;

        public Prensa()
        {
            InitializeComponent();
        }

        public Prensa(long idInforme, string asunto, bool nuevo)
        {
            // TODO: Complete member initialization
            this.idInforme = idInforme;
            this.asunto = asunto;
            this.nuevo = nuevo;
        }
    }
}
