using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Encuesta
    {
        public Encuesta()
        {
            Pregunta = new HashSet<Pregunta>();
        }

        public int IdEncuesta { get; set; }
        public int CreadorEncuesta { get; set; }
        public DateTime FechaEncuesta { get; set; }
        public DateTime FechaFinEncuesta { get; set; }
        public string TituloEncuesta { get; set; }
        public string DescripcionEncuesta { get; set; }

        public Usuario CreadorEncuestaNavigation { get; set; }
        public ICollection<Pregunta> Pregunta { get; set; }
    }
}
