using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Tarea
    {
        public Tarea()
        {
            TareaGrupo = new HashSet<TareaGrupo>();
        }

        public int IdTarea { get; set; }
        public int IdDocente { get; set; }
        public int? IdMateriaTarea { get; set; }
        public string ContenidoTarea { get; set; }

        public Docente IdDocenteNavigation { get; set; }
        public Materia IdMateriaTareaNavigation { get; set; }
        public ICollection<TareaGrupo> TareaGrupo { get; set; }
    }
}
