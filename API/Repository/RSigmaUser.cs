using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class RSigmaUser
    {
        private readonly SigmaContext _context;

        public RSigmaUser(SigmaContext context)
        {
            _context = context;
        }

        public SigmaUser Add(SigmaUser item)
        {
            var user = _context.Add(item);
            _context.SaveChanges();
            return item;
        }
    }
}
