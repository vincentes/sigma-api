
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repository
{
    public class REscritoGrupo : IRepository<EscritoGrupo>
    {
        private readonly SigmaContext _context;

        public REscritoGrupo(SigmaContext context)
        {
            _context = context;
        }

        public EscritoGrupo Add(EscritoGrupo item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            item.Id = entityEntry.Entity.Id;
            return item;
        }

        public void Delete(EscritoGrupo item)
        {
            throw new NotImplementedException();
        }

        public void Update(EscritoGrupo item)
        {
            throw new NotImplementedException();
        }

        IEnumerable<EscritoGrupo> IRepository<EscritoGrupo>.GetAll()
        {
            return _context.EscritoGrupo
                .Include(t => t.Grupo)
                    .ThenInclude(e => e.Turno)
                .Include(t => t.Grupo)
                    .ThenInclude(e => e.Orientacion)
                .Include(t => t.Grupo)
                    .ThenInclude(e => e.GrupoDocentes)
                        .ThenInclude(t => t.Grupo)
                .Include(t => t.Grupo)
                    .ThenInclude(e => e.GrupoDocentes)
                        .ThenInclude(t => t.Docente)
                .Include(t => t.Escrito)
                .ToList();
        }

        EscritoGrupo IRepository<EscritoGrupo>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
