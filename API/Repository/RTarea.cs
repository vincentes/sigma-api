
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repository
{
    public class RTarea : IRepository<Tarea>
    {
        private readonly SigmaContext _context;

        public RTarea(SigmaContext context)
        {
            this._context = context;
        }

        public Tarea Add(Tarea item)
        {
            foreach(TareaImagen ti in item.TareaImagen)
            {
                var tiEntry = _context.Add(ti);
                _context.Add(ti);
            }
    
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            return entityEntry.Entity;
        }

        public void Delete(Tarea item)
        {
            this._context.Remove<Tarea>(item);
            this._context.SaveChanges();
        }

        public IEnumerable<Tarea> GetAll()
        {
            return (IEnumerable<Tarea>)((IIncludableQueryable<Tarea, IEnumerable<TareaImagen>>)this._context.Tareas.Include<Tarea, List<TareaImagen>>((Expression<Func<Tarea, List<TareaImagen>>>)(t => t.TareaImagen))).ThenInclude<Tarea, TareaImagen, Imagen>((Expression<Func<TareaImagen, Imagen>>)(x => x.Imagen)).Include<Tarea, Docente>((Expression<Func<Tarea, Docente>>)(t => t.Docente)).Include<Tarea, Materia>((Expression<Func<Tarea, Materia>>)(t => t.Materia)).ToList<Tarea>();
        }

        public Tarea GetById(int id)
        {
            return ((IIncludableQueryable<Tarea, IEnumerable<TareaImagen>>)this._context.Tareas.Include<Tarea, List<TareaImagen>>((Expression<Func<Tarea, List<TareaImagen>>>)(t => t.TareaImagen))).ThenInclude<Tarea, TareaImagen, Imagen>((Expression<Func<TareaImagen, Imagen>>)(x => x.Imagen)).Include<Tarea, Docente>((Expression<Func<Tarea, Docente>>)(t => t.Docente)).Include<Tarea, Materia>((Expression<Func<Tarea, Materia>>)(t => t.Materia)).SingleOrDefault<Tarea>((Expression<Func<Tarea, bool>>)(x => x.Id == id));
        }

        public void Update(Tarea item)
        {
            Tarea byId = this.GetById(item.Id);
            byId.Id = item.Id;
            this._context.Update<Tarea>(byId);
            this._context.SaveChanges();
        }
    }
}
