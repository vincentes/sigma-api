using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Repository
{
    public class RAdscripto : IUserRepository<Adscripto>
    {
        private readonly SigmaContext _context;

        public RAdscripto(SigmaContext context)
        {
            _context = context;
        }

        public Adscripto Add(Adscripto item)
        {
            var adscripto = _context.Add(item);
            _context.SaveChanges();
            item.Id = adscripto.Entity.Id;
            return item;
        }

        public void Delete(Adscripto item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<Adscripto> GetAll()
        {
            return _context.Adscriptos
                    .ToList();
        }
        
        public Adscripto GetById(string id)
        {
            return _context.Adscriptos
                .SingleOrDefault(x => x.Id == id);
        }

        public void Update(Adscripto item)
        {
            var adscripto = GetById(item.Id);
            _context.Update(adscripto);
            _context.SaveChanges();
        }
    }
}
