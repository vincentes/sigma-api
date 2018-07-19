using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string DocenteId { get; set; }

        public int MateriaId { get; set; }

        public string Contenido { get; set; }

        public List<API.Models.TareaImagen> TareaImagen { get; set; }

        public Docente Docente { get; set; }

        public Materia Materia { get; set; }
    }
}