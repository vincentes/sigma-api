using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Administrador
    {
        public int IdAdministrador { get; set; }
        public int IdUsrAdmin { get; set; }

        public Usuario IdUsrAdminNavigation { get; set; }
    }
}
