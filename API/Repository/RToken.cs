using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repository
{
    public class RToken : IRepository<Token>
    {
        private readonly SigmaContext _context;

        public RToken(SigmaContext context)
        {
            this._context = context;
        }

        public Token Add(Token item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            return entityEntry.Entity;
        }

        public void Delete(Token item)
        {
            this._context.Remove<Token>(item);
            this._context.SaveChanges();
        }

        public void DeleteByUserId(string id)
        {
            List<Token> tokens = _context.Tokens.Include(e => e.User).ToList();
            foreach(Token t in tokens)
            {
                if(t.User.Id == id)
                {
                    Delete(t);
                }
            }
        }

        public IEnumerable<Token> GetAll()
        {
            return _context.Tokens.ToList();
        }

        public Token GetById(int id)
        {
            return _context.Tokens.FirstOrDefault(e => e.Id == id);
        }

        
        public void Update(Token item)
        {
            Token byId = this.GetById(item.Id);
            byId.Id = item.Id;
            this._context.Update<Token>(byId);
            this._context.SaveChanges();
        }
    }
}
