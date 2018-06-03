using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Schedule
    {
        public static void Create(char day, int hour, int subjectID, int professorID, int locID)
        {
            using (SigmaContext db = new SigmaContext())
            {
                HORARIO schedule = new HORARIO();
                schedule.Dia_Horario = char.ToString(day);
                schedule.Hora_Horario = hour;
                schedule.Id_Materia_Horario = subjectID;
                schedule.Id_Docente_Horario = professorID;
                schedule.Id_Salon = locID;
                db.HORARIO.Add(schedule);
                db.SaveChanges();
            }
        }

        public static void CreateTurn(string nombre)
        {
            using(SigmaContext db = new SigmaContext())
            {
                TURNO turn = new TURNO();
                turn.Nombre_Turno = nombre;
                db.TURNO.Add(turn);
                db.SaveChanges();
            }
        }

        public static void CreateHour(TimeSpan start, TimeSpan end)
        {
            using (SigmaContext db = new SigmaContext())
            {
                HORA hour = new HORA();
                hour.Inicio_Hora = start;
                hour.Fin_Hora = end;
                db.HORA.Add(hour);
                db.SaveChanges();
            }
        }

        public static void AssignHour(int hour, int scheduleID, string alias)
        {
            using (SigmaContext db = new SigmaContext())
            {
                HORA_TURNO hs = new HORA_TURNO();
                hs.Turno_HT = scheduleID;
                hs.Alias_HT = alias;
                hs.Hora_HT = hour;
                db.HORA_TURNO.Add(hs);
                db.SaveChanges();
            }
        }
    }
}
