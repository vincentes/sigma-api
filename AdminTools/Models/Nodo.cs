using System;
using System.Collections.Generic;

namespace AdminTools.Models
{
    public partial class Nodo
    {
        public Nodo()
        {
            Beacon = new HashSet<Beacon>();
            Horario = new HashSet<Horario>();
            PonderacionIdNodoActualNavigation = new HashSet<Ponderacion>();
            PonderacionIdNodoAdyacenteNavigation = new HashSet<Ponderacion>();
        }

        public int IdNodo { get; set; }
        public int PosXNodo { get; set; }
        public int PosYNodo { get; set; }
        public string PisoNodo { get; set; }
        public string TipoNodo { get; set; }

        public ICollection<Beacon> Beacon { get; set; }
        public ICollection<Horario> Horario { get; set; }
        public ICollection<Ponderacion> PonderacionIdNodoActualNavigation { get; set; }
        public ICollection<Ponderacion> PonderacionIdNodoAdyacenteNavigation { get; set; }
    }
}
