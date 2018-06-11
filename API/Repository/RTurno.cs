using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class RTurno : IRepository<Turno>
    {
        private readonly SigmaContext _context;

        public RTurno(SigmaContext context)
        {
            _context = context;
        }

        public Turno Add(Turno item)
        {
            var turno = _context.Add(item);
            _context.SaveChanges();
            item.IdTurno = turno.Entity.IdTurno;
            return item;
        }

        public void Delete(Turno item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<Turno> GetAll()
        {
            return _context.Turno.ToList();
        }

        public Turno GetById(int id)
        {
            return _context.Turno.SingleOrDefault(x => x.IdTurno == id);
        }

        public void Update(Turno item)
        {
            var turno = GetById(item.IdTurno);
            turno.NombreTurno = item.NombreTurno;
            _context.Update(turno);
            _context.SaveChanges();
        }
    }
}
