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

namespace ReportCreator
{
    /// <summary>
    /// Lógica de interacción para NuevoBorrador.xaml
    /// </summary>
    public partial class NuevoBorrador : UserControl
    {
        public NuevoBorrador()
        {
            InitializeComponent();
        }

        public NuevoBorrador(long InformeID, bool borrador)
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
