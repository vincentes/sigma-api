using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Prueba
    {
        public int IdPrueba { get; set; }
        public string TipoPrueba { get; set; }
        public int IdMateriaPrueba { get; set; }
        public int IdGrupoPrueba { get; set; }
        public int IdDocentePrueba { get; set; }
        public DateTime FechaPrueba { get; set; }
        public int HoraInicioPrueba { get; set; }
        public int HoraFinPrueba { get; set; }
        public string TemasPrueba { get; set; }

        public HoraTurno HoraFinPruebaNavigation { get; set; }
        public HoraTurno HoraInicioPruebaNavigation { get; set; }
        public Docente IdDocentePruebaNavigation { get; set; }
        public Grupo IdGrupoPruebaNavigation { get; set; }
        public Materia IdMateriaPruebaNavigation { get; set; }
    }
}
