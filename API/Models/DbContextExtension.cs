using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public static class DbContextExtension
    {
        public static void Seed(this SigmaContext context)
        {
            context.Orientaciones.RemoveRange((IEnumerable<Orientacion>)context.Orientaciones);
            context.Materias.RemoveRange((IEnumerable<Materia>)context.Materias);
            context.MateriaOrientacion.RemoveRange((IEnumerable<MateriaOrientacion>)context.MateriaOrientacion);
            context.Grupos.RemoveRange((IEnumerable<Grupo>)context.Grupos);
            context.Turnos.RemoveRange((IEnumerable<Turno>)context.Turnos);
            context.SaveChanges();
            if (context.Orientaciones.Any<Orientacion>())
                return;
            DbContextExtension.Upload(context);
        }

        private static void Upload(SigmaContext context)
        {
            List<Materia> materiaList = new List<Materia>();
            materiaList.AddRange((IEnumerable<Materia>)new List<Materia>((IEnumerable<Materia>)new Materia[5]
            {
        new Materia() { Nombre = "Literatura" },
        new Materia() { Nombre = "Matemática" },
        new Materia() { Nombre = "Inglés" },
        new Materia() { Nombre = "Educación Ciudadana" },
        new Materia() { Nombre = "Filosofía" }
            }));
            context.Materias.AddRange((IEnumerable<Materia>)materiaList);
            context.SaveChanges();
            Orientacion entity1 = new Orientacion()
            {
                Nombre = "Científico"
            };
            context.Orientaciones.Add(entity1);
            context.SaveChanges();
            foreach (Materia materia in materiaList)
            {
                MateriaOrientacion entity2 = new MateriaOrientacion()
                {
                    OrientacionId = entity1.Id,
                    MateriaId = materia.Id
                };
                context.MateriaOrientacion.Add(entity2);
                context.SaveChanges();
            }
            Materia[] materiaArray = new Materia[4]
            {
        new Materia() { Nombre = "Física" },
        new Materia() { Nombre = "Matemática II" },
        new Materia() { Nombre = "Comunicación Visual" },
        new Materia() { Nombre = "Química" }
            };
            foreach (Materia entity2 in new List<Materia>((IEnumerable<Materia>)materiaArray))
            {
                context.Materias.Add(entity2);
                context.SaveChanges();
                MateriaOrientacion entity3 = new MateriaOrientacion()
                {
                    OrientacionId = entity1.Id,
                    MateriaId = entity2.Id
                };
                context.MateriaOrientacion.Add(entity3);
                context.SaveChanges();
            }
            Turno entity4 = new Turno();
            entity4.Nombre = "Matutino";
            context.Turnos.Add(entity4);
            context.SaveChanges();
            context.Grupos.Add(new Grupo()
            {
                Anio = 2018,
                Grado = 1,
                Numero = 1,
                OrientacionId = entity1.Id,
                TurnoId = entity4.Id
            });

            context.Salones.Add(new Salon()
            {
                Nombre = "Salón 31"
            });
            context.Salones.Add(new Salon()
            {
                Nombre = "Salón 32"
            });
            context.Salones.Add(new Salon()
            {
                Nombre = "Salón 33"
            });
            var salon = new Salon()
            {
                Nombre = "Salón 34"
            };
            context.Salones.Add(salon);

            context.Grupos.Add(new Grupo()
            {
                Anio = 2018,
                Grado = 5,
                Numero = 2,
                Turno = entity4,
                Orientacion = entity1
            });

            context.SaveChanges();

            context.HoraMaterias.Add(new HoraMateria()
            {
                Hora = 1,
                Materia = materiaArray[0],
                Salon = salon
            });

            context.HoraMaterias.Add(new HoraMateria()
            {
                Hora = 2,
                Materia = materiaArray[1],
                Salon = salon
            });

            context.HoraMaterias.Add(new HoraMateria()
            {
                Hora = 3,
                Materia = materiaArray[2],
                Salon = salon
            });


            context.SaveChanges();
        }
    }
}
