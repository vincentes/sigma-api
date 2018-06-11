using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Notificacion
    {
        public Notificacion()
        {
            NotificacionDestinatario = new HashSet<NotificacionDestinatario>();
        }

        public int IdNotificacion { get; set; }
        public string DestinoNotificacion { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public string TituloNotificacion { get; set; }
        public string MensajeNotificacion { get; set; }

        public ICollection<NotificacionDestinatario> NotificacionDestinatario { get; set; }
    }
}
