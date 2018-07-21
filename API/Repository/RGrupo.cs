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
    public class RGrupo : IRepository<Grupo>
    {
        private readonly SigmaContext _context;

        public RGrupo(SigmaContext context)
        {
            this._context = context;
        }

        public Grupo Add(Grupo item)
        {
            EntityEntry<Grupo> entityEntry = this._context.Add<Grupo>(item);
            this._context.SaveChanges();
            item.Id = entityEntry.Entity.Id;
            return item;
        }

        public void Delete(Grupo item)
        {
            this._context.Remove<Grupo>(item);
            this._context.SaveChanges();
        }

        public IEnumerable<Grupo> GetAll()
        {
            return (IEnumerable<Grupo>)((IIncludableQueryable<Grupo, IEnumerable<GrupoDocente>>)((IIncludableQueryable<Grupo, IEnumerable<GrupoDocente>>)this._context.Grupos.Include<Grupo, Turno>((Expression<Func<Grupo, Turno>>)(e => e.Turno)).Include<Grupo, Orientacion>((Expression<Func<Grupo, Orientacion>>)(e => e.Orientacion)).Include<Grupo, ICollection<GrupoDocente>>((Expression<Func<Grupo, ICollection<GrupoDocente>>>)(e => e.GrupoDocentes))).ThenInclude<Grupo, GrupoDocente, Grupo>((Expression<Func<GrupoDocente, Grupo>>)(t => t.Grupo)).Include<Grupo, ICollection<GrupoDocente>>((Expression<Func<Grupo, ICollection<GrupoDocente>>>)(e => e.GrupoDocentes))).ThenInclude<Grupo, GrupoDocente, Docente>((Expression<Func<GrupoDocente, Docente>>)(t => t.Docente)).ToList<Grupo>();
        }

        public Grupo GetById(int id)
        {
            return ((IIncludableQueryable<Grupo, IEnumerable<GrupoDocente>>)((IIncludableQueryable<Grupo, IEnumerable<GrupoDocente>>)this._context.Grupos.Include<Grupo, Turno>((Expression<Func<Grupo, Turno>>)(e => e.Turno)).Include<Grupo, Orientacion>((Expression<Func<Grupo, Orientacion>>)(e => e.Orientacion)).Include<Grupo, ICollection<GrupoDocente>>((Expression<Func<Grupo, ICollection<GrupoDocente>>>)(e => e.GrupoDocentes))).ThenInclude<Grupo, GrupoDocente, Grupo>((Expression<Func<GrupoDocente, Grupo>>)(t => t.Grupo)).Include<Grupo, ICollection<GrupoDocente>>((Expression<Func<Grupo, ICollection<GrupoDocente>>>)(e => e.GrupoDocentes))).ThenInclude<Grupo, GrupoDocente, Docente>((Expression<Func<GrupoDocente, Docente>>)(t => t.Docente)).SingleOrDefault<Grupo>((Expression<Func<Grupo, bool>>)(x => x.Id == id));
        }

        public void Update(Grupo item)
        {
            Grupo byId = this.GetById(item.Id);
            byId.Id = item.Id;
            byId.Grado = item.Grado;
            byId.OrientacionId = item.OrientacionId;
            byId.TurnoId = item.TurnoId;
            byId.Numero = item.Numero;
            this._context.Update<Grupo>(byId);
            this._context.SaveChanges();
        }
    }
}
