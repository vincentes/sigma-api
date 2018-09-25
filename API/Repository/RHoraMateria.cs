using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class RHoraMateria : IRepository<HoraMateria>
    {
        private readonly SigmaContext _context;

        public RHoraMateria(SigmaContext context)
        {
            this._context = context;
        }

        public HoraMateria Add(HoraMateria item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            var excludeEscritoEntity = entityEntry.Entity;
            return excludeEscritoEntity;
        }

        public void Delete(HoraMateria item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HoraMateria> GetAll()
        {
            return _context.HoraMaterias
                    .Include(p => p.Grupo)
                        .ThenInclude(e => e.Turno)
                    .Include(p => p.Salon)
                    .Include(p => p.Materia)
                    .ToList();
        }

        public HoraMateria GetById(int id)
        {
            return _context.HoraMaterias
                     .Include(p => p.Grupo)
                     .Include(p => p.Grupo)
                        .ThenInclude(e => e.Turno)
                    .Include(p => p.Salon)
                    .Include(p => p.Materia)
                    .SingleOrDefault(x => x.Id == id);
        }

        public void Update(HoraMateria item)
        {
            _context.Update(item);
            _context.SaveChanges();
        }
    }
}
