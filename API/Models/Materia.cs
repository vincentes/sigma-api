using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Materia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public virtual ICollection<Docente> Docentes { get; set; }

        public virtual ICollection<API.Models.MateriaOrientacion> MateriaOrientacion { get; set; }

        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
