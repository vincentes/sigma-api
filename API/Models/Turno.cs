using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Turno
    {
        public Turno()
        {
            Adscripto = new HashSet<Adscripto>();
            Grupo = new HashSet<Grupo>();
            HoraTurno = new HashSet<HoraTurno>();
        }

        public int IdTurno { get; set; }
        public string NombreTurno { get; set; }

        public ICollection<Adscripto> Adscripto { get; set; }
        public ICollection<Grupo> Grupo { get; set; }
        public ICollection<HoraTurno> HoraTurno { get; set; }
    }
}
