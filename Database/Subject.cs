using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Subject
    {
        public static void Create(string nombre, int orientacionID)
        {
            using(SigmaContext db = new SigmaContext())
            {
                MATERIA materia = new MATERIA();
                materia.Nombre_Materia = nombre;
                materia.Orientacion_Materia = orientacionID;
                db.MATERIA.Add(materia);
                db.SaveChanges();
            }
        }
    }
}
