using System;
using System.Collections.Generic;

namespace AdminTools.Models
{
    public partial class HoraTurno
    {
        public HoraTurno()
        {
            Horario = new HashSet<Horario>();
            PruebaHoraFinPruebaNavigation = new HashSet<Prueba>();
            PruebaHoraInicioPruebaNavigation = new HashSet<Prueba>();
        }

        public int IdHt { get; set; }
        public int HoraHt { get; set; }
        public int TurnoHt { get; set; }
        public string AliasHt { get; set; }

        public Hora HoraHtNavigation { get; set; }
        public Turno TurnoHtNavigation { get; set; }
        public ICollection<Horario> Horario { get; set; }
        public ICollection<Prueba> PruebaHoraFinPruebaNavigation { get; set; }
        public ICollection<Prueba> PruebaHoraInicioPruebaNavigation { get; set; }
    }
}
