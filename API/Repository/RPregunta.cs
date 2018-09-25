
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
    public class RPregunta : IRepository<Pregunta>
    {
        private readonly SigmaContext _context;

        public RPregunta(SigmaContext context)
        {
            this._context = context;
        }

        public Pregunta Add(Pregunta item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            var excludePreguntaEntity = entityEntry.Entity;
            return excludePreguntaEntity;
        }

        public void Delete(Pregunta item)
        {
            this._context.Remove<Pregunta>(item);
            this._context.SaveChanges();
        }

        public IEnumerable<Pregunta> GetAll()
        {
            return _context.Preguntas
                        .Include(e=>(e as PreguntaVariada).Respuestas)
                        .ToList();
        }

        public Pregunta GetById(int id)
        {
            return _context.Preguntas         
                        .Include(e=>(e as PreguntaVariada).Respuestas)
                        .SingleOrDefault(x => x.Id == id);
        }

        public void Update(Pregunta item)
        {
            this._context.Update<Pregunta>(item);
            this._context.SaveChanges();
        }
    }
}
