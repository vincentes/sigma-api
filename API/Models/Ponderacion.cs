using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Ponderacion
    {
        public int IdNodoActual { get; set; }
        public int IdNodoAdyacente { get; set; }
        public int Ponderacion1 { get; set; }

        public Nodo IdNodoActualNavigation { get; set; }
        public Nodo IdNodoAdyacenteNavigation { get; set; }
    }
}
