using System;
using System.Collections.Generic;

namespace AdminTools.Models
{
    public partial class Administrador
    {
        public int IdAdministrador { get; set; }
        public int IdUsrAdmin { get; set; }

        public Usuario IdUsrAdminNavigation { get; set; }
    }
}
