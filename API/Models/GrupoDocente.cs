using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class GrupoDocente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GrupoId { get; set; }
        public string DocenteId { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual Docente Docente { get; set; }
    }
}
