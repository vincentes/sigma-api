using API.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repository
{
    public class RTurno : IRepository<Turno>
    {
        private readonly SigmaContext _context;

        public RTurno(SigmaContext context)
        {
            this._context = context;
        }

        public Turno Add(Turno item)
        {
            EntityEntry<Turno> entityEntry = this._context.Add<Turno>(item);
            this._context.SaveChanges();
            item.Id = entityEntry.Entity.Id;
            return item;
        }

        public void Delete(Turno item)
        {
            this._context.Remove<Turno>(item);
            this._context.SaveChanges();
        }

        public IEnumerable<Turno> GetAll()
        {
            return (IEnumerable<Turno>)this._context.Turnos.ToList<Turno>();
        }

        public Turno GetById(int id)
        {
            return this._context.Turnos.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Turno item)
        {
            Turno byId = this.GetById(item.Id);
            byId.Nombre = item.Nombre;
            this._context.Update<Turno>(byId);
            this._context.SaveChanges();
        }
    }
}
