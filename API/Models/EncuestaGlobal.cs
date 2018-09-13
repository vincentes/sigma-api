using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class EncuestaGlobal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string AdscriptoId { get; set; }
        public Adscripto Adscripto { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string Descripcion { get; set; }
        public List<Pregunta> Preguntas { get; set; }
    }
}
