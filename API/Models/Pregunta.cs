using API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public enum TipoPregunta
    {
        Free = 0,
        Radio = 1,
        Select = 2,
        CheckBox = 3
    }

    public class Pregunta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Texto { get; set; }
        public int EncuestaId { get; set; }
        public EncuestaGlobal Encuesta { get; set; }
    }
    
    public class PreguntaVariada : Pregunta
    {
        public List<PreguntaOpcion> Opciones { get; set; }
        public List<RespuestaLimitada> Respuestas { get; set; }
    }

    public class PreguntaLibre : Pregunta
    {
        public List<RespuestaLibre> Respuestas { get; set; }
    }

    public class PreguntaMO : PreguntaVariada
    {

    }

    public class PreguntaEL : PreguntaLibre
    {
    }

    public class PreguntaUO : PreguntaVariada
    {
    }

    public class Respuesta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PreguntaId { get; set; }
        public string AlumnoId { get; set; }
        public Alumno Alumno { get; set; }
        [ForeignKey("PreguntaId")]
        public Pregunta Pregunta { get; set; }
    }

    public class RespuestaLibre : Respuesta
    {
        public string Texto { get; set; }
    }

    public class RespuestaLimitada : Respuesta
    {

    }

    public class RespuestaMO : RespuestaLimitada
    {
        public List<PreguntaOpcion> RespuestaOpciones { get; set; }
        public List<OpcionRespuesta> Respuestas { get; set; }
    }

    public class RespuestaUO : RespuestaLimitada
    {
        public int RespuestaOpcionId { get; set; }
        public PreguntaOpcion RespuestaOpcion { get; set; }
    }

    public class OpcionRespuesta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OpcionId { get; set; }
        public PreguntaOpcion Opcion { get; set; }
        public int RespuestaId { get; set; }
        public RespuestaMO Respuesta { get; set; }
    }
}
