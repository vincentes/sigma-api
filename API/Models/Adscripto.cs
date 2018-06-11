using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Adscripto
    {
        public int IdAdscripto { get; set; }
        public int IdUsrAdscripto { get; set; }
        public int TurnoAdscripto { get; set; }

        public Usuario IdUsrAdscriptoNavigation { get; set; }
        public Turno TurnoAdscriptoNavigation { get; set; }
    }
}
