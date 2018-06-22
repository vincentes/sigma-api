using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class RDocente : IUserRepository<Docente>
    {
        private readonly SigmaContext _context;

        public RDocente(SigmaContext context)
        {
            _context = context;
        }

        public Docente Add(Docente item)
        {
            var docente = _context.Add(item);
            _context.SaveChanges();
            item.Id = docente.Entity.Id;
            return item;
        }

        public void Delete(Docente item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<Docente> GetAll()
        {
            return _context.Docentes.ToList();
        }
        
        public Docente GetById(string id)
        {
            return _context.Docentes.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Docente item)
        {
            var docente = GetById(item.Id);
            docente.Email = item.Email;
            docente.MateriaId = item.MateriaId;
            _context.Update(docente);
            _context.SaveChanges();
        }
    }
}
