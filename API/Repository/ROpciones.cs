
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
    public class RPreguntaOpcion : IRepository<PreguntaOpcion>
    {
        private readonly SigmaContext _context;

        public RPreguntaOpcion(SigmaContext context)
        {
            this._context = context;
        }

        public PreguntaOpcion Add(PreguntaOpcion item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            var excludePreguntaOpcionEntity = entityEntry.Entity;
            return excludePreguntaOpcionEntity;
        }

        public void Delete(PreguntaOpcion item)
        {
            this._context.Remove<PreguntaOpcion>(item);
            this._context.SaveChanges();
        }

        public IEnumerable<PreguntaOpcion> GetAll()
        {
            return _context.PreguntaOpciones
                        .ToList();
        }

        public PreguntaOpcion GetById(int id)
        {
            return _context.PreguntaOpciones
                        .SingleOrDefault(x => x.Id == id);
        }

        public void Update(PreguntaOpcion item)
        {
            PreguntaOpcion byId = this.GetById(item.Id);
            byId.Id = item.Id;
            this._context.Update<PreguntaOpcion>(byId);
            this._context.SaveChanges();
        }
    }
}
