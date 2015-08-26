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
        public ObservableCollection<PrensaUO> prensas { get; set; }
        public ObservableCollection<Suscripcion> suscripciones { get; set; }
    }
}
