using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class REvent : IRepository<Event>
    {
        private readonly SigmaContext _context;

        public REvent(SigmaContext context)
        {
            this._context = context;
        }

        public Event Add(Event item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            var excludeEscritoEntity = entityEntry.Entity;
            return excludeEscritoEntity;
        }

        public void Delete(Event item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Events.ToList();
        }

        public Event GetById(int id)
        {
            throw new NotImplementedException();

        }

        public void Update(Event item)
        {
            throw new NotImplementedException();
        }
    }
}
