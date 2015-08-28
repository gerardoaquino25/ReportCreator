using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ReportCreator.Utilities
{
    public class Validador
    {
        public IList<object[]> objectosAValidar;
        public IList<object[]> errores;
        public IList<object[]> correctos;

        public Validador()
        {
            objectosAValidar = new List<object[]>();
            errores = new List<object[]>();
            correctos = new List<object[]>();
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
                if (Validar(objecto[1]))
                    correctos.Add(new object[] { objecto[0], (string)objecto[3] });
                else
                    errores.Add(new object[] { objecto[0], (string)objecto[2] });
            }
        }

        private bool Validar(object objecto)
        {
            if (objecto == null)
                return false;
            else
            {
                if (objecto.GetType() == typeof(ComboBox))
                    return Validar((ComboBox)objecto);

                if (objecto.GetType() == typeof(ListBox))
                    return Validar((ListBox)objecto);

                if (objecto.GetType() == typeof(TextBox))
                    return Validar((TextBox)objecto);

                return false;
            }
        }

        private bool Validar(ComboBox combobox)
        {
            if (combobox.SelectedItem == null)
                return false;
            else
                return true;
        }

        private bool Validar(ListBox listbox)
        {
            if (listbox.SelectedItem == null)
                return false;
            else
                return true;
        }

        private bool Validar(TextBox textbox)
        {
            if (String.IsNullOrWhiteSpace(textbox.Text))
                return false;
            else
                return true;
        }
    }
}
