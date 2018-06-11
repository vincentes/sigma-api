using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class TareaGrupo
    {
        public int IdTg { get; set; }
        public int IdGrupoTg { get; set; }
        public DateTime FechaTg { get; set; }
        public DateTime FechaEntregaTg { get; set; }

        public Grupo IdGrupoTgNavigation { get; set; }
        public Tarea IdTgNavigation { get; set; }
    }
}
