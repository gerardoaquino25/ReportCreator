﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class Entrada
    {
        public long? id { get; set; }
        public long? informeId { get; set; }
        public Tipo tipo { get; set; }
        public string titulo { get; set; }
        public string tipoDescripcion { get; set; }
    }
}
