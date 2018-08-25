using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Repository
{
    public class RAlumno : IUserRepository<Alumno>
    {
        private readonly SigmaContext _context;

        public RAlumno(SigmaContext context)
        {
            _context = context;
        }

        public Alumno Add(Alumno item)
        {
            var alumno = _context.Add(item);
            _context.SaveChanges();
            item.Id = alumno.Entity.Id;
            return item;
        }

        public void Delete(Alumno item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<Alumno> GetAll()
        {
            return _context.Alumnos
                    .Include(t => t.Grupo)
                        .ThenInclude(j => j.TareaGrupo)
                            .ThenInclude(x => x.Tarea)
                    .Include(t => t.Grupo)
                        .ThenInclude(j => j.TareaGrupo)
                            .ThenInclude(x => x.Tarea)
                                .ThenInclude(y => y.TareaImagen)
                                    .ThenInclude(z => z.Imagen)
                    .Include(t => t.Grupo)
                        .ThenInclude(j => j.TareaGrupo)
                            .ThenInclude(x => x.Tarea)
                                .ThenInclude(y => y.Docente)
                   .Include(t => t.Grupo)
                        .ThenInclude(j => j.TareaGrupo)
                            .ThenInclude(x => x.Tarea)
                                .ThenInclude(y => y.Materia)
                    .ToList();
        }
        
        public Alumno GetById(string id)
        {
            return _context.Alumnos
                .Include(t => t.Grupo)
                    .ThenInclude(j => j.TareaGrupo)
                        .ThenInclude(x => x.Tarea)
                .Include(t => t.Grupo)
                    .ThenInclude(j => j.TareaGrupo)
                        .ThenInclude(x => x.Tarea)
                            .ThenInclude(y => y.TareaImagen)
                                .ThenInclude(z => z.Imagen)
                .Include(t => t.Grupo)
                    .ThenInclude(j => j.TareaGrupo)
                        .ThenInclude(x => x.Tarea)
                            .ThenInclude(y => y.Docente)
                .Include(t => t.Grupo)
                    .ThenInclude(j => j.TareaGrupo)
                        .ThenInclude(x => x.Evento)
                            .ThenInclude(y => y.GruposAsignados)
                .SingleOrDefault(x => x.Id == id);
        }

        public void Update(Alumno item)
        {
            var alumno = GetById(item.Id);
            _context.Update(alumno);
            _context.SaveChanges();
        }
    }
}
