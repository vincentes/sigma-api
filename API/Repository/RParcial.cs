
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
    public class RParcial : IRepository<Parcial>
    {
        private readonly SigmaContext _context;

        public RParcial(SigmaContext context)
        {
            this._context = context;
        }

        public Parcial Add(Parcial item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            var excludeParcialEntity = entityEntry.Entity;
            excludeParcialEntity.Docente = null;
            return excludeParcialEntity;
        }

        public void Delete(Parcial item)
        {
            this._context.Remove<Parcial>(item);
            this._context.SaveChanges();
        }

        public IEnumerable<Parcial> GetAll()
        {
            return _context.Parciales
                        .Include(t => t.Docente)
                        .Include(t => t.Materia)
                        .Include(t => t.GruposAsignados)
                            .ThenInclude(p => p.Grupo)
                                .ThenInclude(x => x.Alumnos)
                                    .ThenInclude(z => z.Token)
                        .ToList();
        }

        public Parcial GetById(int id)
        {
            return _context.Parciales                 
                        .Include(t => t.Docente)
                        .Include(t => t.Materia)
                        .Include(t => t.GruposAsignados)
                            .ThenInclude(p => p.Grupo)
                                .ThenInclude(x => x.Alumnos)
                                    .ThenInclude(z => z.Token)
                        .SingleOrDefault(x => x.Id == id);
        }

        public void Update(Parcial item)
        {
            Parcial byId = this.GetById(item.Id);
            byId.Id = item.Id;
            this._context.Update<Parcial>(byId);
            this._context.SaveChanges();
        }
    }
}
