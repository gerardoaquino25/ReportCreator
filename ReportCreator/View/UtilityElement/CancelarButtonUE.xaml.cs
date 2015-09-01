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

namespace ReportCreator.View.UtilityElement
{
    /// <summary>
    /// Lógica de interacción para CancelarButtonUE.xaml
    /// </summary>
    public partial class CancelarButtonUE : UserControl
    {
        public CancelarButtonUE()
        {
            InitializeComponent();
        }

        private void Cancelar_MouseEnter(object sender, MouseEventArgs e)
        {
            Contenedor.Cursor = Cursors.Hand;
        }

        private void Cancelar_MouseLeave(object sender, MouseEventArgs e)
        {
            Contenedor.Cursor = Cursors.Arrow;
        }
    }
}
