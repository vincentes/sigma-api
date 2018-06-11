using System;
using System.Collections.Generic;

namespace AdminTools.Models
{
    public partial class NotificacionDestinatario
    {
        public int IdNd { get; set; }
        public int IdDestinatario { get; set; }
        public int? GrupoActualUsr { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime? FechaEntrega { get; set; }

        public Grupo GrupoActualUsrNavigation { get; set; }
        public Usuario IdDestinatarioNavigation { get; set; }
        public Notificacion IdNdNavigation { get; set; }
    }
}
