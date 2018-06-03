using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public enum UserType
    {
        Admin,
        Pertinent,
        Professor,
        Student
    }

    public class User
    {
        public static bool Create(string cedula, string password, string phone, string token, string name, string surname)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
{
                    new SqlParameter {ParameterName="@pCedulaUsr", Value=cedula, Direction = System.Data.ParameterDirection.Input},
                    new SqlParameter {ParameterName="@pPassword", Value=password, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter {ParameterName="@pCelular_Usr", Value=phone, Direction = System.Data.ParameterDirection.Input},
                    new SqlParameter {ParameterName="@pToken_Usr", Value=Utils.RandomString(50), Direction = System.Data.ParameterDirection.Input},
                    new SqlParameter {ParameterName="@pNombre", Value=name, Direction = System.Data.ParameterDirection.Input},
                    new SqlParameter {ParameterName="@pApellido", Value=surname, Direction = System.Data.ParameterDirection.Input},
                    new SqlParameter {ParameterName="@mensajeRespuesta", Value=-1, Direction = System.Data.ParameterDirection.Output}
};

            ObjectParameter response = new ObjectParameter("mensajeRespuesta", typeof(string));
            using (var db = new SigmaContext())
            {
                db.PR_Ingresar_Usuario(cedula, password, cedula, token, name, surname, response);
                db.SaveChanges();
            } 
            return (string) response.Value == "INGRESO_USUARIO_OK";
        }

        public static bool Login(string cedula, string password)
        {
            ObjectParameter response = new ObjectParameter("mensajeRespuesta", typeof(string));
            using (var db = new SigmaContext())
            {
                db.PR_Login(cedula, password, response);
                db.SaveChanges();
            }
            return (string)response.Value == "OK";
        }



        public static bool CreateStudent(string cedula, string password, string phone, string token, string name, string surname, int grupoID)
        {
            if(!Create(cedula, password, phone, token, name, surname)) {
                return false;
            }
            

            ALUMNO student = new ALUMNO();
            using(SigmaContext db = new SigmaContext())
            {
                foreach(var u in db.USUARIO.ToList())
                {
                    if(u.Cedula_Usr.Equals(cedula))
                    {
                        student.USUARIO = u;
                        student.Id_Usr_Alumno = u.Id_Usr;
                        student.Grupo_Usr = grupoID;
                        db.ALUMNO.Add(student);
                        db.SaveChanges();
                    }
                }
            }

            return true;
        }

        public static bool CreateProfessor(string cedula, string password, string phone, string token, string name, string surname, int materiaID)
        {
            if (!Create(cedula, password, phone, token, name, surname))
            {
                return false;
            }


            using (SigmaContext db = new SigmaContext())
            {
                foreach (var u in db.USUARIO.ToList())
                {
                    if (u.Cedula_Usr.Equals(cedula))
                    {
                        DOCENTE docente = new DOCENTE();
                        docente.USUARIO = u;
                        docente.Materia_Docente = materiaID;
                        db.DOCENTE.Add(docente);
                        db.SaveChanges();
                    }
                }
            }

            return true;
        }


        public static bool CreateAdscripto(string cedula, string password, string phone, string token, string name, string surname, int turnoID)
        {
            if (!Create(cedula, password, phone, token, name, surname))
            {
                return false;
            }


            using (SigmaContext db = new SigmaContext())
            {
                foreach (var u in db.USUARIO.ToList())
                {
                    if (u.Cedula_Usr.Equals(cedula))
                    {
                        ADSCRIPTO ads = new ADSCRIPTO();
                        ads.USUARIO = u;
                        ads.Turno_Adscripto = turnoID;
                        db.ADSCRIPTO.Add(ads);
                        db.SaveChanges();
                    }
                }
            }

            return true;
        }


        public static bool CreateAdmin(string cedula, string password, string phone, string token, string name, string surname)
        {
            if (!Create(cedula, password, phone, token, name, surname))
            {
                return false;
            }


            using (SigmaContext db = new SigmaContext())
            {
                foreach (var u in db.USUARIO.ToList())
                {
                    if (u.Cedula_Usr.Equals(cedula))
                    {
                        ADMINISTRADOR admin = new ADMINISTRADOR();
                        admin.USUARIO = u;
                        db.ADMINISTRADOR.Add(admin);
                        db.SaveChanges();
                    }
                }
            }

            return true;
        }
    }
}
