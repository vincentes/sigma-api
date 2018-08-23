
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
            var excludeDocenteEntity = entityEntry.Entity;
            excludeDocenteEntity.Docente = null;
            return excludeDocenteEntity;
        }

        public void Delete(Tarea item)
        {
            this._context.Remove<Tarea>(item);
            this._context.SaveChanges();
        }

        public IEnumerable<Tarea> GetAll()
        {
            return _context.Tareas
                .Include(t => t.TareaImagen)
                    .ThenInclude(x => x.Imagen)
                .Include(t => t.Docente)
                .Include(t => t.Materia)
                .ToList();
        }

        public Tarea GetById(int id)
        {
            return _context.Tareas.Include(t => t.TareaImagen).ThenInclude(x => x.Imagen).Include(t => t.Docente).Include(t => t.Materia)
                .Include(t => t.TareaGrupos)
                    .ThenInclude(p => p.Grupo)
                .Include(t => t.TareaGrupos)
                    .ThenInclude(p => p.Evento)
                .SingleOrDefault(x => x.Id == id);
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
