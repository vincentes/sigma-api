
using API.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repository
{
    public class RGrupoDocente : IRepository<GrupoDocente>
    {
        private readonly SigmaContext _context;

        public RGrupoDocente(SigmaContext context)
        {
            this._context = context;
        }

        public GrupoDocente Add(GrupoDocente item)
        {
            EntityEntry<GrupoDocente> entityEntry = this._context.Add<GrupoDocente>(item);
            this._context.SaveChanges();
            item.Id = entityEntry.Entity.Id;
            return item;
        }

        public void Delete(GrupoDocente item)
        {
            this._context.Remove<GrupoDocente>(item);
            this._context.SaveChanges();
        }

        public IEnumerable<GrupoDocente> GetAll()
        {
            return (IEnumerable<GrupoDocente>)this._context.GrupoDocente.ToList<GrupoDocente>();
        }

        public GrupoDocente GetById(int id)
        {
            return this._context.GrupoDocente.SingleOrDefault<GrupoDocente>((Expression<Func<GrupoDocente, bool>>)(x => x.Id == id));
        }

        public void Update(GrupoDocente item)
        {
            GrupoDocente byId = this.GetById(item.Id);
            byId.DocenteId = item.DocenteId;
            byId.GrupoId = item.GrupoId;
            this._context.Update<GrupoDocente>(byId);
            this._context.SaveChanges();
        }
    }
}
