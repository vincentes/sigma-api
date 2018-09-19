using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class PreguntaOpcion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Valor { get; set; }
        public int PreguntaId { get; set; }
        [ForeignKey("PreguntaId")]
        public PreguntaVariada Pregunta { get; set; }
        public List<RespuestaLimitada> RespuestasAsociadas { get; set; }
        public List<OpcionRespuesta> OpcionRespuestas { get; set; }
    }
}