using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ROrientacion : IRepository<Orientacion>
    {
        private readonly SigmaContext _context;

        public ROrientacion(SigmaContext context)
        {
            _context = context;
        }

        public Orientacion Add(Orientacion item)
        {
            var orientacion = _context.Add(item);
            _context.SaveChanges();
            item.Id = orientacion.Entity.Id;
            return item;
        }

        public void Delete(Orientacion item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<Orientacion> GetAll()
        {
            return _context.Orientaciones.Include(t => t.Materias).Include(t => t.Grupos).ToList();
        }

        public Orientacion GetById(int id)
        {
            return _context.Orientaciones.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Orientacion item)
        {
            var orientacion = GetById(item.Id);
            orientacion.Nombre = item.Nombre;
            _context.Update(orientacion);
            _context.SaveChanges();
        }
    }
}
