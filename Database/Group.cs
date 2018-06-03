using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Group
    {
        public static bool Create(int grado, int branch, int number, int scheduleID, int year)
        {
            GRUPO group = new GRUPO();
            using (SigmaContext db = new SigmaContext())
            {
                group.Grado_Grupo = grado;
                group.Orientacion_Grupo = branch;
                group.Numero_Grupo = number;
                group.Turno_Grupo = scheduleID;
                group.Anio_grupo = year;
                db.GRUPO.Add(group);
                db.SaveChanges();
            }
            return group.Id_Grupo != 0;
        }
    }
}
