using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Docente : AppUser
    {
        public int MateriaId { get; set; }
        public virtual ICollection<Grupo> Grupos { get; set; }
        public virtual ICollection<GrupoDocente> GrupoDocentes { get; set; }
        public virtual Materia Materia { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
