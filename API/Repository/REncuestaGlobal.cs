using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class REncuestaGlobal : IRepository<EncuestaGlobal>
    {
        private readonly SigmaContext _context;

        public REncuestaGlobal(SigmaContext context)
        {
            _context = context;
        }

        public EncuestaGlobal Add(EncuestaGlobal item)
        {
            var encuesta = _context.Add(item);
            _context.SaveChanges();
            item.Id = encuesta.Entity.Id;
            return item;
        }

        public void Delete(EncuestaGlobal item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<EncuestaGlobal> GetAll()
        {
            return _context.EncuestasGlobales
                .Include(e => e.Preguntas)
                 .ToList();
        }

        public EncuestaGlobal GetById(int id)
        {
            return _context.EncuestasGlobales
                .Include(e => e.Preguntas)
                 .SingleOrDefault(e => e.Id == id);
        }

        public void Update(EncuestaGlobal item)
        {
            var encuesta = GetById(item.Id);
            encuesta.Preguntas = item.Preguntas;
            _context.Update(encuesta);
            _context.SaveChanges();
        }

    }
}
