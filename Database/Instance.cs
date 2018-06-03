using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Instance
    {
        public static void CreateHomework(int docenteID, int materiaID, string content)
        {
            using(SigmaContext db = new SigmaContext())
            {
                TAREA homework = new TAREA();
                homework.Id_Docente = docenteID;
                homework.Id_Materia_Tarea = materiaID;
                homework.Contenido_Tarea = content;
                db.TAREA.Add(homework);
                db.SaveChanges();
            }
        }

        public static void CreateTest(char type, int subjectID, int groupID, int professorID, DateTime date, int startTime, int endTime, string temas)
        {
            using (SigmaContext db = new SigmaContext())
            {
                PRUEBA test = new PRUEBA();
                test.Fecha_Prueba = date;
                test.Hora_Inicio_Prueba = startTime;
                test.Hora_Fin_Prueba = endTime;
                test.Id_Docente_Prueba = professorID;
                test.Id_Materia_Prueba = subjectID;
                test.Id_Grupo_Prueba = groupID;
                test.Tipo_Prueba = char.ToString(type);
                test.Temas_Prueba = temas;
                db.PRUEBA.Add(test);
                db.SaveChanges();
            }
        }

        public static void AssignFile(int homeworkID, string url)
        {
            using(SigmaContext db = new SigmaContext())
            {
                TAREA_ADJUNTO file = new TAREA_ADJUNTO();
                file.Id_Tarea_TA = homeworkID;
                file.Url_Adjunto = url;
                db.TAREA_ADJUNTO.Add(file);
                db.SaveChanges();
            }
        }

        public static void AssignToGroup(int groupID, DateTime date, DateTime deadline)
        {
            using (SigmaContext db = new SigmaContext())
            {
                TAREA_GRUPO assignment = new TAREA_GRUPO();
                assignment.Id_Grupo_TG = groupID;
                assignment.Fecha_TG = date;
                assignment.Fecha_Entrega_TG = deadline;
                db.TAREA_GRUPO.Add(assignment);
                db.SaveChanges();
            }
        }
    }
}
