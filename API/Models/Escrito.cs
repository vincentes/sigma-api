using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Escrito : Event
    {
        public string DocenteId { get; set; }

        public int MateriaId { get; set; }

        public string Temas { get; set; }

        [ForeignKey("DocenteId")]
        public Docente Docente { get; set; }

        [ForeignKey("MateriaId")]
        public Materia Materia { get; set; }
        public List<EscritoGrupo> GruposAsignados { get; set; }

    }
}