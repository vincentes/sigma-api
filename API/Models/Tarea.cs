using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Tarea : Event
    {
        public string DocenteId { get; set; }

        public int MateriaId { get; set; }

        public string Contenido { get; set; }

        public List<TareaImagen> TareaImagen { get; set; }

        public Docente Docente { get; set; }

        public Materia Materia { get; set; }

    }
}