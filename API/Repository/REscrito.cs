
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
    public class REscrito : IRepository<Escrito>
    {
        private readonly SigmaContext _context;

        public REscrito(SigmaContext context)
        {
            this._context = context;
        }

        public Escrito Add(Escrito item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            var excludeEscritoEntity = entityEntry.Entity;
            excludeEscritoEntity.Docente = null;
            return excludeEscritoEntity;
        }

        public void Delete(Escrito item)
        {
            this._context.Remove<Escrito>(item);
            this._context.SaveChanges();
        }

        public IEnumerable<Escrito> GetAll()
        {
            return _context.Escritos
                        .Include(t => t.Docente)
                        .Include(t => t.Materia)
                        .Include(t => t.GruposAsignados)
                            .ThenInclude(p => p.Grupo)
                                .ThenInclude(x => x.Alumnos)
                                    .ThenInclude(z => z.Token)
                        .ToList();
        }

        public Escrito GetById(int id)
        {
            return _context.Escritos                 
                        .Include(t => t.Docente)
                        .Include(t => t.Materia)
                        .Include(t => t.GruposAsignados)
                            .ThenInclude(p => p.Grupo)
                                .ThenInclude(x => x.Alumnos)
                                    .ThenInclude(z => z.Token)
                        .SingleOrDefault(x => x.Id == id);
        }

        public void Update(Escrito item)
        {
            Escrito byId = this.GetById(item.Id);
            byId.Id = item.Id;
            this._context.Update<Escrito>(byId);
            this._context.SaveChanges();
        }
    }
}
