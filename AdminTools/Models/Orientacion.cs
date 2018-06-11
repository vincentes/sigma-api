using System;
using System.Collections.Generic;

namespace AdminTools.Models
{
    public partial class Orientacion
    {
        public Orientacion()
        {
            Grupo = new HashSet<Grupo>();
            Materia = new HashSet<Materia>();
        }

        public int IdOrientacion { get; set; }
        public string NombreOrientacion { get; set; }

        public ICollection<Grupo> Grupo { get; set; }
        public ICollection<Materia> Materia { get; set; }
    }
}
