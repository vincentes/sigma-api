using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Notification
    {
        public static void Create(char destination, DateTime date, string title, string message)
        {
            using(SigmaContext db = new SigmaContext())
            {
                NOTIFICACION noti = new NOTIFICACION();
                noti.Destino_Notificacion = char.ToString(destination).ToUpper();
                noti.Fecha_Notificacion = date;
                noti.Titulo_Notificacion = title;
                noti.Mensaje_Notificacion = message;
                db.NOTIFICACION.Add(noti);
                db.SaveChanges();
            }
        }

        public static void CreateReceived(int notificationID, int userID, int groupID, DateTime sent, DateTime received)
        {
            using (SigmaContext db = new SigmaContext())
            {
                NOTIFICACION_DESTINATARIO noti = new NOTIFICACION_DESTINATARIO();
                noti.Id_ND = notificationID;
                noti.Id_Destinatario = userID;
                noti.Grupo_Actual_Usr = groupID;
                noti.Fecha_Envio = sent;
                noti.Fecha_Entrega = received;
                db.NOTIFICACION_DESTINATARIO.Add(noti);
                db.SaveChanges();
            }
        }
    }
}
