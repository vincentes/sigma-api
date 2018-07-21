
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
    public class RImagen : IRepository<Imagen>
    {
        private readonly SigmaContext _context;

        public RImagen(SigmaContext context)
        {
            this._context = context;
        }

        public Imagen Add(Imagen item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            return entityEntry.Entity;
        }

        public void Delete(Imagen item)
        {
            this._context.Remove<Imagen>(item);
            this._context.SaveChanges();
        }

        public IEnumerable<Imagen> GetAll()
        {
            return (IEnumerable<Imagen>)((IIncludableQueryable<Imagen, IEnumerable<TareaImagen>>)((IIncludableQueryable<Imagen, IEnumerable<TareaImagen>>)this._context.Imagen.Include<Imagen, ICollection<TareaImagen>>((Expression<Func<Imagen, ICollection<TareaImagen>>>)(t => t.TareaImagen))).ThenInclude<Imagen, TareaImagen, Tarea>((Expression<Func<TareaImagen, Tarea>>)(x => x.Tarea)).Include<Imagen, ICollection<TareaImagen>>((Expression<Func<Imagen, ICollection<TareaImagen>>>)(t => t.TareaImagen))).ThenInclude<Imagen, TareaImagen, Imagen>((Expression<Func<TareaImagen, Imagen>>)(x => x.Imagen)).ToList<Imagen>();
        }

        public Imagen GetById(int id)
        {
            return ((IIncludableQueryable<Imagen, IEnumerable<TareaImagen>>)((IIncludableQueryable<Imagen, IEnumerable<TareaImagen>>)this._context.Imagen.Include<Imagen, ICollection<TareaImagen>>((Expression<Func<Imagen, ICollection<TareaImagen>>>)(t => t.TareaImagen))).ThenInclude<Imagen, TareaImagen, Tarea>((Expression<Func<TareaImagen, Tarea>>)(x => x.Tarea)).Include<Imagen, ICollection<TareaImagen>>((Expression<Func<Imagen, ICollection<TareaImagen>>>)(t => t.TareaImagen))).ThenInclude<Imagen, TareaImagen, Imagen>((Expression<Func<TareaImagen, Imagen>>)(x => x.Imagen)).SingleOrDefault<Imagen>((Expression<Func<Imagen, bool>>)(x => x.Id == id));
        }

        public void Update(Imagen item)
        {
            Imagen byId = this.GetById(item.Id);
            byId.Id = item.Id;
            this._context.Update<Imagen>(byId);
            this._context.SaveChanges();
        }
    }
}
