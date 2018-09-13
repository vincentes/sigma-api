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
    }

    public class PreguntaMO
    {
        public List<Respuesta> Respuestas { get; set; }
    }

    public class PreguntaUO
    {
        public Respuesta Respuesta { get; set; }
    }
}
