using System;
using System.Collections.Generic;

namespace AdminTools.Models
{
    public partial class Materia
    {
        public Materia()
        {
            Docente = new HashSet<Docente>();
            Horario = new HashSet<Horario>();
            Prueba = new HashSet<Prueba>();
            Tarea = new HashSet<Tarea>();
        }

        public int IdMateria { get; set; }
        public string NombreMateria { get; set; }
        public int OrientacionMateria { get; set; }

        public Orientacion OrientacionMateriaNavigation { get; set; }
        public ICollection<Docente> Docente { get; set; }
        public ICollection<Horario> Horario { get; set; }
        public ICollection<Prueba> Prueba { get; set; }
        public ICollection<Tarea> Tarea { get; set; }
    }
}
