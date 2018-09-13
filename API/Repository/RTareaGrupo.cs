
using API.Models;
using Microsoft.EntityFrameworkCore;
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
            return _context.TareaGrupo
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
                .Include(t => t.Tarea)
                .ToList();
        }

        TareaGrupo IRepository<TareaGrupo>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
