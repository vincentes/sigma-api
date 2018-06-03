using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Branch
    {
        public static bool Create(string name)
        {
            ORIENTACION branch = new ORIENTACION();
            using (SigmaContext db = new SigmaContext())
            {
                branch.Nombre_Orientacion = name;
                db.ORIENTACION.Add(branch);
                db.SaveChanges();
            }
            return branch.Id_Orientacion != 0;
        }
    }
}
