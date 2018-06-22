using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class RMateria : IRepository<Materia>
    {
        private readonly SigmaContext _context;

        public RMateria(SigmaContext context)
        {
            _context = context;
        }

        public Materia Add(Materia item)
        {
            var materia = _context.Add(item);
            _context.SaveChanges();
            item.Id = materia.Entity.Id;
            return item;
        }

        public void Delete(Materia item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<Materia> GetAll()
        {
            return _context.Materias.ToList();
        }

        public Materia GetById(int id)
        {
            return _context.Materias.Include(t => t.Orientacion).Include(t => t.Docentes).SingleOrDefault(x => x.Id == id);
        }

        public void Update(Materia item)
        {
            var materia = GetById(item.Id);
            materia.Id = item.Id;
            materia.Nombre = item.Nombre;
            materia.OrientacionId = item.OrientacionId;
            _context.Update(materia);
            _context.SaveChanges();
        }
    }
}
