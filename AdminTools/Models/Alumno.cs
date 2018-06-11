using System;
using System.Collections.Generic;

namespace AdminTools.Models
{
    public partial class Alumno
    {
        public int IdAlumno { get; set; }
        public int IdUsrAlumno { get; set; }
        public int GrupoUsr { get; set; }

        public Grupo GrupoUsrNavigation { get; set; }
        public Usuario IdUsrAlumnoNavigation { get; set; }
    }
}
