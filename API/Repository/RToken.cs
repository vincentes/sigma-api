// Decompiled with JetBrains decompiler
// Type: API.Repository.RToken
// Assembly: API, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B418147-8FFB-41A2-8EEF-9BE2FCA642AC
// Assembly location: C:\Users\micro\Documents\decompiling\API.dll

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
            EntityEntry<Token> entityEntry = this._context.Add<Token>(item);
            this._context.SaveChanges();
            item.Id = entityEntry.Entity.Id;
            return item;
        }

        public void Delete(Token item)
        {
            this._context.Remove<Token>(item);
            this._context.SaveChanges();
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
