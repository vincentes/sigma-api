using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Grupo
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Grado { get; set; }
        public int Numero { get; set; }
        public int Anio { get; set; }
        public int OrientacionId { get; set; }
        public int TurnoId { get; set; }
        public virtual Orientacion Orientacion { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual ICollection<GrupoDocente> GrupoDocentes { get; set; }
    }
}
