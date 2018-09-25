using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class RSalon : IRepository<Salon>
    {
        private readonly SigmaContext _context;

        public RSalon(SigmaContext context)
        {
            this._context = context;
        }

        public Salon Add(Salon item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            var excludeEscritoEntity = entityEntry.Entity;
            return excludeEscritoEntity;
        }

        public void Delete(Salon item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Salon> GetAll()
        {
            return _context.Salones
                    .ToList();
        }

        public Salon GetById(int id)
        {
            return _context.Salones
                    .SingleOrDefault(x => x.Id == id);
        }

        public void Update(Salon item)
        {
            _context.Update<Salon>(item);
            _context.SaveChanges();
        }
    }
}
