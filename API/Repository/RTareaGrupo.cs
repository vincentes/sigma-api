
using API.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repository
{
    public class RTareaGrupo : IRepository<TareaGrupo>
    {
        private readonly SigmaContext _context;

        public RTareaGrupo(SigmaContext context)
        {
            _context = context;
        }

        public TareaGrupo Add(TareaGrupo item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            item.Id = entityEntry.Entity.Id;
            return item;
        }

        public void Delete(TareaGrupo item)
        {
            throw new NotImplementedException();
        }

        public void Update(TareaGrupo item)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TareaGrupo> IRepository<TareaGrupo>.GetAll()
        {
            throw new NotImplementedException();
        }

        TareaGrupo IRepository<TareaGrupo>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
