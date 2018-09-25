using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class RAlerta : IRepository<Alerta>
    {
        private readonly SigmaContext _context;

        public RAlerta(SigmaContext context)
        {
            this._context = context;
        }

        public Alerta Add(Alerta item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            var excludeEscritoEntity = entityEntry.Entity;
            return excludeEscritoEntity;
        }

        public void Delete(Alerta item)
        {
            _context.Alertas.RemoveRange((IEnumerable<Alerta>)_context.Alertas);
        }

        public IEnumerable<Alerta> GetAll()
        {
            return _context.Alertas
                    .ToList();
        }

        public Alerta GetById(int id)
        {
            return _context.Alertas
                    .SingleOrDefault(x => x.Id == id);
        }

        public void Update(Alerta item)
        {
            _context.Update(item);
            _context.SaveChanges();
        }
    }
}
