using System;
using System.Collections.Generic;

namespace AdminTools.Models
{
    public partial class RespuestaEncuesta
    {
        public int IdUsuarioEncuestado { get; set; }
        public int IdPreguntaRespondida { get; set; }
        public int IdRespuesta { get; set; }
        public int IdRespustaEncuesta { get; set; }

        public Pregunta IdPreguntaRespondidaNavigation { get; set; }
        public OpcionPregunta IdRespuestaNavigation { get; set; }
        public Usuario IdUsuarioEncuestadoNavigation { get; set; }
    }
}
