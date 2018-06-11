using System;
using System.Collections.Generic;

namespace AdminTools.Models
{
    public partial class Docente
    {
        public Docente()
        {
            Horario = new HashSet<Horario>();
            Prueba = new HashSet<Prueba>();
            Tarea = new HashSet<Tarea>();
        }

        public int IdDocente { get; set; }
        public int IdUsrDocente { get; set; }
        public int MateriaDocente { get; set; }

        public Usuario IdUsrDocenteNavigation { get; set; }
        public Materia MateriaDocenteNavigation { get; set; }
        public ICollection<Horario> Horario { get; set; }
        public ICollection<Prueba> Prueba { get; set; }
        public ICollection<Tarea> Tarea { get; set; }
    }
}
