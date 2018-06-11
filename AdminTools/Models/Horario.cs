using System;
using System.Collections.Generic;

namespace AdminTools.Models
{
    public partial class Horario
    {
        public int IdGrupoHorario { get; set; }
        public string DiaHorario { get; set; }
        public int HoraHorario { get; set; }
        public int IdMateriaHorario { get; set; }
        public int IdDocenteHorario { get; set; }
        public int IdSalon { get; set; }

        public HoraTurno HoraHorarioNavigation { get; set; }
        public Docente IdDocenteHorarioNavigation { get; set; }
        public Grupo IdGrupoHorarioNavigation { get; set; }
        public Materia IdMateriaHorarioNavigation { get; set; }
        public Nodo IdSalonNavigation { get; set; }
    }
}
