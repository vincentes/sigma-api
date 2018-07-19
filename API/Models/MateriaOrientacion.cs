using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MateriaOrientacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MateriaId { get; set; }

        public int OrientacionId { get; set; }

        public virtual Materia Materia { get; set; }

        public virtual Orientacion Orientacion { get; set; }
    }
}
