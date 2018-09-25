using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API.Utils
{
    public static class Firebase
    {
        public static void RemindEvents(ParcialGrupo pg)
        {
            DateTime now = DateTime.Now;
            DateTime deadline = pg.Date;
            int daysDifference = (deadline - now).Days;
            string dayName = deadline.ToString("dddd", new CultureInfo("es-ES"));

            if (daysDifference <= 5)
            {
                string title;
                string body;
                DateTime sent = DateTime.Now;

                if (daysDifference == 1)
                {
                    title = "Parcial próximo";
                    body = "¡Tenés un parcial mañana!";
                }
                else
                {
                    title = "Parcial próximo";
                    body = "¡Tenés un parcial este " + dayName + "!";
                }


                foreach (Alumno alumno in pg.Grupo.Alumnos)
                {
                    foreach (Token token in alumno.Token)
                    {
                        SendNotification(token.Content, title, body);
                    }
                }
            }
        }

        public static void RemindEvents(EscritoGrupo pg)
        {
            DateTime now = DateTime.Now;
            DateTime deadline = pg.Date;
            int daysDifference = (deadline - now).Days;
            string dayName = deadline.ToString("dddd", new CultureInfo("es-ES"));

            if (daysDifference <= 5)
            {
                string title;
                string body;
                DateTime sent = DateTime.Now;

                if (daysDifference == 1)
                {
                    title = "Escrito próximo";
                    body = "¡Tenés un escrito mañana!";
                }
                else
                {
                    title = "Escrito próximo";
                    body = "¡Tenés un escrito este " + dayName + "!";
                }


                foreach (Alumno alumno in pg.Grupo.Alumnos)
                {
                    foreach (Token token in alumno.Token)
                    {
                        SendNotification(token.Content, title, body);
                    }
                }
            }
        }

        public static void RemindEvents(TareaGrupo pg)
        {
            DateTime now = DateTime.Now;
            DateTime deadline = pg.Date;
            int daysDifference = (deadline - now).Days;
            string dayName = deadline.ToString("dddd", new CultureInfo("es-ES"));

            if (daysDifference <= 5)
            {
                string title;
                string body;
                DateTime sent = DateTime.Now;

                if (daysDifference == 1)
                {
                    title = "Entrega próxima";
                    body = "¡Tenés que entregar un deber mañana!";
                }
                else
                {
                    title = "Entrega próxima";
                    body = "¡Tenés que entregar un deber este " + dayName + "!";
                }

                foreach (Alumno alumno in pg.Grupo.Alumnos)
                {
                    foreach (Token token in alumno.Token)
                    {
                        SendNotification(token.Content, title, body);
                    }
                }
            }
        }

        public static void NotifyCreated(Parcial parcial)
        {
            foreach (ParcialGrupo grupo in parcial.GruposAsignados)
            {
                SendGroupNotification(grupo, "Nuevo parcial", "Un profesor fijó una fecha de parcial para tu grupo.");
            }
        }

        public static void NotifyCreated(Escrito escrito)
        {
            foreach (EscritoGrupo grupo in escrito.GruposAsignados)
            {
                SendGroupNotification(grupo, "Escrito nuevo", "Un profesor fijó una fecha de escrito para tu grupo.");
            }
        }

        public static void NotifyCreated(Tarea tarea)
        {
            foreach (TareaGrupo grupo in tarea.GruposAsignados)
            {
                SendGroupNotification(grupo, "Asignación de deber", "Un profesor te ha asignado un deber.");
            }
        }

        public static void SendGroupNotification(TareaGrupo tg, string title, string body)
        {
            foreach (Alumno alumno in tg.Grupo.Alumnos)
            {
                if(alumno.Token != null)
                {
                    foreach (Token token in alumno.Token)
                    {
                        SendNotification(token.Content, title, body);
                    }
                }
            }
        }

        public static void SendGroupNotification(ParcialGrupo tg, string title, string body)
        {
            foreach (Alumno alumno in tg.Grupo.Alumnos)
            {
                if (alumno.Token != null)
                {
                    foreach (Token token in alumno.Token)
                    {
                        SendNotification(token.Content, title, body);
                    }
                }
            }
        }

        public static void SendGroupNotification(EscritoGrupo tg, string title, string body)
        {
            foreach (Alumno alumno in tg.Grupo.Alumnos)
            {
                if (alumno.Token != null)
                {
                    foreach (Token token in alumno.Token)
                    {
                        SendNotification(token.Content, title, body);
                    }
                }
            }
        }

        public static void SendNotification(string token, string title, string body)
        {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAO1RqYb0:APA91bFBNSUfhsJzjKqv-uO6cPTjpW2R39MIWqKiC5eyqgrAVpUxv67306up7Edm_19RQMGGKc3pyX6ZswgRYsGms3cyeYbUmAZEzgYJ5zPN0m2AkaA9CQqq_R5obPJRj5-dKqUNidSh"));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "254819328445"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = token,
                priority = "high",
                content_available = true,
                notification = new
                {
                    title,
                    body,
                    badge = 1
                },
            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
        }
    }
}
