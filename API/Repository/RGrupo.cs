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
            return _context.Grupos.Include(e => e.Turno).Include(e => e.Orientacion)
                .Include(e => e.GrupoDocentes)
                    .ThenInclude(t => t.Grupo)
                .Include(e => e.GrupoDocentes)
                    .ThenInclude(t => t.Docente)
                .ToList();
        }

        public Grupo GetById(int id)
        {
            return _context.Grupos
                .Include(e => e.Turno)
                .Include(e => e.Orientacion)
                .Include(e => e.GrupoDocentes)
                    .ThenInclude(t => t.Grupo)
                .Include(e => e.GrupoDocentes)
                    .ThenInclude(t => t.Docente)
                .SingleOrDefault(x => x.Id == id);
        }

        public void Update(Grupo item)
        {
            Grupo byId = this.GetById(item.Id);
            byId.Id = item.Id;
            byId.Grado = item.Grado;
            byId.OrientacionId = item.OrientacionId;
            byId.TurnoId = item.TurnoId;
            byId.Numero = item.Numero;
            byId.Alumnos = item.Alumnos;
            _context.Update<Grupo>(byId);
            _context.SaveChanges();
        }
    }
}
