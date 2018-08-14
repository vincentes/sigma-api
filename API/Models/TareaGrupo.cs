using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class TareaGrupo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public Tarea Tarea { get; set; }
        public int TareaId { get; set; }
        public Grupo Grupo { get; set; }
        public int GrupoId { get; set; }
    }
}
