using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Beacon
    {
        public string IdBeacon { get; set; }
        public int IdNodoBeacon { get; set; }
        public string StatusNodo { get; set; }

        public Nodo IdNodoBeaconNavigation { get; set; }
    }
}
