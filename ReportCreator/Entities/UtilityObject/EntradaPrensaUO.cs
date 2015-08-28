using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportCreator.Entities;
using System.Collections.ObjectModel;

namespace ReportCreator.Entities.UtilityObject
{
    public class EntradaPrensaUO : Entrada
    {
        public ObservableCollection<PrensaOB> prensas { get; set; }
        public ObservableCollection<SuscripcionOB> suscripciones { get; set; }

        public EntradaPrensaUO()
        {
            this.prensas = new ObservableCollection<PrensaOB>();
            this.suscripciones = new ObservableCollection<SuscripcionOB>();
        }
    }
}
