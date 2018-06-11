using System;
using System.Collections.Generic;

namespace AdminTools.Models
{
    public partial class Pregunta
    {
        public Pregunta()
        {
            OpcionPregunta = new HashSet<OpcionPregunta>();
            RespuestaEncuesta = new HashSet<RespuestaEncuesta>();
        }

        public int IdPregunta { get; set; }
        public int IdEncuestaPregunta { get; set; }
        public string TextoPregunta { get; set; }

        public Encuesta IdEncuestaPreguntaNavigation { get; set; }
        public ICollection<OpcionPregunta> OpcionPregunta { get; set; }
        public ICollection<RespuestaEncuesta> RespuestaEncuesta { get; set; }
    }
}
