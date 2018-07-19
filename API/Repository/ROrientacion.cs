// Decompiled with JetBrains decompiler
// Type: API.Repository.ROrientacion
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
    public class ROrientacion : IRepository<Orientacion>
    {
        private readonly SigmaContext _context;

        public ROrientacion(SigmaContext context)
        {
            this._context = context;
        }

        public Orientacion Add(Orientacion item)
        {
            EntityEntry<Orientacion> entityEntry = this._context.Add<Orientacion>(item);
            this._context.SaveChanges();
            item.Id = entityEntry.Entity.Id;
            return item;
        }

        public void Delete(Orientacion item)
        {
            this._context.Remove<Orientacion>(item);
            this._context.SaveChanges();
        }

        public IEnumerable<Orientacion> GetAll()
        {
            return (IEnumerable<Orientacion>)((IIncludableQueryable<Orientacion, IEnumerable<MateriaOrientacion>>)((IIncludableQueryable<Orientacion, IEnumerable<MateriaOrientacion>>)((IIncludableQueryable<Orientacion, IEnumerable<Grupo>>)this._context.Orientaciones.Include<Orientacion, ICollection<Grupo>>((Expression<Func<Orientacion, ICollection<Grupo>>>)(t => t.Grupos))).ThenInclude<Orientacion, Grupo, Turno>((Expression<Func<Grupo, Turno>>)(x => x.Turno)).Include<Orientacion, ICollection<MateriaOrientacion>>((Expression<Func<Orientacion, ICollection<MateriaOrientacion>>>)(t => t.MateriaOrientacion))).ThenInclude<Orientacion, MateriaOrientacion, Orientacion>((Expression<Func<MateriaOrientacion, Orientacion>>)(x => x.Orientacion)).Include<Orientacion, ICollection<MateriaOrientacion>>((Expression<Func<Orientacion, ICollection<MateriaOrientacion>>>)(t => t.MateriaOrientacion))).ThenInclude<Orientacion, MateriaOrientacion, Materia>((Expression<Func<MateriaOrientacion, Materia>>)(x => x.Materia)).ToList<Orientacion>();
        }

        public Orientacion GetById(int id)
        {
            return ((IIncludableQueryable<Orientacion, IEnumerable<MateriaOrientacion>>)((IIncludableQueryable<Orientacion, IEnumerable<MateriaOrientacion>>)((IIncludableQueryable<Orientacion, IEnumerable<Grupo>>)this._context.Orientaciones.Include<Orientacion, ICollection<Grupo>>((Expression<Func<Orientacion, ICollection<Grupo>>>)(t => t.Grupos))).ThenInclude<Orientacion, Grupo, Turno>((Expression<Func<Grupo, Turno>>)(x => x.Turno)).Include<Orientacion, ICollection<MateriaOrientacion>>((Expression<Func<Orientacion, ICollection<MateriaOrientacion>>>)(t => t.MateriaOrientacion))).ThenInclude<Orientacion, MateriaOrientacion, Orientacion>((Expression<Func<MateriaOrientacion, Orientacion>>)(x => x.Orientacion)).Include<Orientacion, ICollection<MateriaOrientacion>>((Expression<Func<Orientacion, ICollection<MateriaOrientacion>>>)(t => t.MateriaOrientacion))).ThenInclude<Orientacion, MateriaOrientacion, Materia>((Expression<Func<MateriaOrientacion, Materia>>)(x => x.Materia)).SingleOrDefault<Orientacion>((Expression<Func<Orientacion, bool>>)(x => x.Id == id));
        }

        public void Update(Orientacion item)
        {
            Orientacion byId = this.GetById(item.Id);
            byId.Nombre = item.Nombre;
            this._context.Update<Orientacion>(byId);
            this._context.SaveChanges();
        }
    }
}
