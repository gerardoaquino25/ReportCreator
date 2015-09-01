using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ReportCreator.Utilities
{
    public class Validador
    {
        public IList<object[]> objectosAValidar;
        public IList<object[]> errores;
        public IList<object[]> correctos;
        public string contenedor1;
        public string contenedor2;

        public Validador(string contenedor1, string contenedor2)
        {
            objectosAValidar = new List<object[]>();
            errores = new List<object[]>();
            correctos = new List<object[]>();
            this.contenedor1 = contenedor1;
            this.contenedor2 = contenedor2;
        }

        public void Add(DockPanel panel, ComboBox objectoAValidar, string respuestaNegativa = "KO", string respuestaPositiva = "OK")
        {
            this.objectosAValidar.Add(new object[] { panel, objectoAValidar, respuestaNegativa, respuestaPositiva });
        }

        public void Add(DockPanel panel, TextBox objectoAValidar, string respuestaNegativa = "KO", string respuestaPositiva = "OK")
        {
            this.objectosAValidar.Add(new object[] { panel, objectoAValidar, respuestaNegativa, respuestaPositiva });
        }

        public void Add(DockPanel panel, ListBox objectoAValidar, string respuestaNegativa = "KO", string respuestaPositiva = "OK")
        {
            this.objectosAValidar.Add(new object[] { panel, objectoAValidar, respuestaNegativa, respuestaPositiva });
        }

        public void Validar()
        {
            foreach (object[] objecto in objectosAValidar)
            {
                if (Validar(objecto[1], objecto))
                {
                    correctos.Add(new object[] { objecto[0], (string)objecto[3] });

                    foreach (UIElement ele in ((DockPanel)objecto[0]).Children)
                    {
                        if (ele.Uid == contenedor1)
                        {
                            foreach (UIElement ele2 in ((StackPanel)ele).Children)
                            {
                                if (ele2.Uid == contenedor2)
                                {
                                    ele2.Visibility = Visibility.Collapsed;
                                    ((Label)ele2).Content = ((string)objecto[3]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    errores.Add(new object[] { objecto[0], (string)objecto[2] });

                    foreach (UIElement ele in ((DockPanel)objecto[0]).Children)
                    {
                        if (ele.Uid == contenedor1)
                        {
                            foreach (UIElement ele2 in ((StackPanel)ele).Children)
                            {
                                if (ele2.Uid == contenedor2)
                                {
                                    ele2.Visibility = Visibility.Visible;
                                    ((Label)ele2).Content = ((string)objecto[2]);
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool Validar(object objecto, object objecto2)
        {
            if (objecto == null)
                return false;
            else
            {
                if (objecto.GetType() == typeof(ComboBox))
                    return Validar((ComboBox)objecto, objecto2);

                if (objecto.GetType() == typeof(ListBox))
                    return Validar((ListBox)objecto, objecto2);

                if (objecto.GetType() == typeof(TextBox))
                    return Validar((TextBox)objecto, objecto2);

                return false;
            }
        }

        private bool Validar(ComboBox combobox, object objecto2)
        {
            if (combobox.SelectedItem == null)
            {
                combobox.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                combobox.BorderBrush = new SolidColorBrush(Colors.LightGray);
                return true;
            }
        }

        private bool Validar(ListBox listbox, object objecto2)
        {
            if (listbox.SelectedItem == null)
            {
                listbox.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                listbox.BorderBrush = new SolidColorBrush(Colors.LightGray);
                return true;
            }
        }

        private bool Validar(TextBox textbox, object objecto2)
        {
            if (String.IsNullOrWhiteSpace(textbox.Text))
            {
                textbox.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                textbox.BorderBrush = new SolidColorBrush(Colors.LightGray);
                return true;
            }
        }
    }
}
