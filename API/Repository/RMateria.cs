// Decompiled with JetBrains decompiler
// Type: API.Repository.RMateria
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
    public class RMateria : IRepository<Materia>
    {
        private readonly SigmaContext _context;

        public RMateria(SigmaContext context)
        {
            this._context = context;
        }

        public Materia Add(Materia item)
        {
            EntityEntry<Materia> entityEntry = this._context.Add<Materia>(item);
            this._context.SaveChanges();
            item.Id = entityEntry.Entity.Id;
            return item;
        }

        public void Delete(Materia item)
        {
            this._context.Remove<Materia>(item);
            this._context.SaveChanges();
        }

        public IEnumerable<Materia> GetAll()
        {
            return (IEnumerable<Materia>)((IIncludableQueryable<Materia, IEnumerable<MateriaOrientacion>>)((IIncludableQueryable<Materia, IEnumerable<Grupo>>)((IIncludableQueryable<Materia, IEnumerable<MateriaOrientacion>>)this._context.Materias.Include<Materia, ICollection<MateriaOrientacion>>((Expression<Func<Materia, ICollection<MateriaOrientacion>>>)(t => t.MateriaOrientacion))).ThenInclude<Materia, MateriaOrientacion, ICollection<Grupo>>((Expression<Func<MateriaOrientacion, ICollection<Grupo>>>)(x => x.Orientacion.Grupos))).ThenInclude<Materia, Grupo, Turno>((Expression<Func<Grupo, Turno>>)(y => y.Turno)).Include<Materia, ICollection<MateriaOrientacion>>((Expression<Func<Materia, ICollection<MateriaOrientacion>>>)(t => t.MateriaOrientacion))).ThenInclude<Materia, MateriaOrientacion, ICollection<Docente>>((Expression<Func<MateriaOrientacion, ICollection<Docente>>>)(x => x.Materia.Docentes)).Include<Materia, ICollection<Docente>>((Expression<Func<Materia, ICollection<Docente>>>)(t => t.Docentes)).ToList<Materia>();
        }

        public Materia GetById(int id)
        {
            return ((IIncludableQueryable<Materia, IEnumerable<MateriaOrientacion>>)((IIncludableQueryable<Materia, IEnumerable<MateriaOrientacion>>)this._context.Materias.Include<Materia, ICollection<MateriaOrientacion>>((Expression<Func<Materia, ICollection<MateriaOrientacion>>>)(t => t.MateriaOrientacion))).ThenInclude<Materia, MateriaOrientacion, ICollection<Grupo>>((Expression<Func<MateriaOrientacion, ICollection<Grupo>>>)(x => x.Orientacion.Grupos)).Include<Materia, ICollection<MateriaOrientacion>>((Expression<Func<Materia, ICollection<MateriaOrientacion>>>)(t => t.MateriaOrientacion))).ThenInclude<Materia, MateriaOrientacion, ICollection<Docente>>((Expression<Func<MateriaOrientacion, ICollection<Docente>>>)(x => x.Materia.Docentes)).Include<Materia, ICollection<Docente>>((Expression<Func<Materia, ICollection<Docente>>>)(t => t.Docentes)).SingleOrDefault<Materia>((Expression<Func<Materia, bool>>)(x => x.Id == id));
        }

        public void Update(Materia item)
        {
            Materia byId = this.GetById(item.Id);
            byId.Id = item.Id;
            byId.Nombre = item.Nombre;
            this._context.Update<Materia>(byId);
            this._context.SaveChanges();
        }
    }
}
