using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Grupo
    {
        public Grupo()
        {
            Alumno = new HashSet<Alumno>();
            Horario = new HashSet<Horario>();
            NotificacionDestinatario = new HashSet<NotificacionDestinatario>();
            Prueba = new HashSet<Prueba>();
            TareaGrupo = new HashSet<TareaGrupo>();
        }

        public int IdGrupo { get; set; }
        public int GradoGrupo { get; set; }
        public int OrientacionGrupo { get; set; }
        public int NumeroGrupo { get; set; }
        public int? TurnoGrupo { get; set; }
        public int AnioGrupo { get; set; }

        public Orientacion OrientacionGrupoNavigation { get; set; }
        public Turno TurnoGrupoNavigation { get; set; }
        public ICollection<Alumno> Alumno { get; set; }
        public ICollection<Horario> Horario { get; set; }
        public ICollection<NotificacionDestinatario> NotificacionDestinatario { get; set; }
        public ICollection<Prueba> Prueba { get; set; }
        public ICollection<TareaGrupo> TareaGrupo { get; set; }
    }
}
