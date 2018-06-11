using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class OpcionPregunta
    {
        public OpcionPregunta()
        {
            RespuestaEncuesta = new HashSet<RespuestaEncuesta>();
        }

        public int IdOp { get; set; }
        public int IdPreguntaOp { get; set; }
        public string TextoOp { get; set; }

        public Pregunta IdPreguntaOpNavigation { get; set; }
        public ICollection<RespuestaEncuesta> RespuestaEncuesta { get; set; }
    }
}
