// Decompiled with JetBrains decompiler
// Type: API.Repository.RTarea
// Assembly: API, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B418147-8FFB-41A2-8EEF-9BE2FCA642AC
// Assembly location: C:\Users\micro\Documents\decompiling\API.dll

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
            EntityEntry<Tarea> entityEntry = this._context.Add<Tarea>(item);
            this._context.SaveChanges();
            item.Id = entityEntry.Entity.Id;
            return item;
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
