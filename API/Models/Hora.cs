using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Hora
    {
        public Hora()
        {
            HoraTurno = new HashSet<HoraTurno>();
        }

        public int IdHora { get; set; }
        public TimeSpan InicioHora { get; set; }
        public TimeSpan FinHora { get; set; }

        public ICollection<HoraTurno> HoraTurno { get; set; }
    }
}
