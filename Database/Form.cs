using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Form
    {
        public static void Create(int userID, DateTime start, DateTime end, string title, string description)
        {
            using(SigmaContext db = new SigmaContext())
            {
                ENCUESTA encuesta = new ENCUESTA();
                encuesta.Creador_Encuesta = userID;
                encuesta.Descripcion_Encuesta = description;
                encuesta.Fecha_Encuesta = start;
                encuesta.Fecha_Fin_Encuesta = end;
                encuesta.Titulo_Encuesta = title;
                db.ENCUESTA.Add(encuesta);
                db.SaveChanges();
            }
        }

        public static void AddQuestion(int form, string question)
        {
            using (SigmaContext db = new SigmaContext())
            {
                PREGUNTA q = new PREGUNTA();
                q.Id_Encuesta_Pregunta = form;
                q.Texto_Pregunta = question;
                db.PREGUNTA.Add(q);
                db.SaveChanges();
            }
        }

        public static void AddOption(int form, int question, string text)
        {
            using (SigmaContext db = new SigmaContext())
            {
                OPCION_PREGUNTA option = new OPCION_PREGUNTA();
                option.Id_Pregunta_OP = question;
                option.Texto_OP = text;
                db.OPCION_PREGUNTA.Add(option);
                db.SaveChanges();
            }
        }

        public static void AddResponse(int userID, int questionID, int optionID)
        {
            using (SigmaContext db = new SigmaContext())
            {
                RESPUESTA_ENCUESTA answer = new RESPUESTA_ENCUESTA();
                answer.Id_Usuario_Encuestado = userID;
                answer.Id_Pregunta_Respondida = questionID;
                answer.Id_Respuesta = optionID;
                db.RESPUESTA_ENCUESTA.Add(answer);
                db.SaveChanges();
            }
        }
    }
}
