
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repository
{
    public class RParcialGrupo : IRepository<ParcialGrupo>
    {
        private readonly SigmaContext _context;

        public RParcialGrupo(SigmaContext context)
        {
            _context = context;
        }

        public ParcialGrupo Add(ParcialGrupo item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            item.Id = entityEntry.Entity.Id;
            return item;
        }

        public void Delete(ParcialGrupo item)
        {
            throw new NotImplementedException();
        }

        public void Update(ParcialGrupo item)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ParcialGrupo> IRepository<ParcialGrupo>.GetAll()
        {
            return _context.ParcialGrupo
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
                .Include(t => t.Parcial)
                .ToList();
        }

        ParcialGrupo IRepository<ParcialGrupo>.GetById(int parcialId)
        {
            return _context.ParcialGrupo
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
                .Include(t => t.Parcial)
                .SingleOrDefault(x => x.ParcialId == parcialId);
        }

        public List<ParcialGrupo> GetAssignedGroups(int parcialId)
        {
            return _context.ParcialGrupo
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
                .Include(t => t.Parcial)
                .Where(x => x.ParcialId == parcialId)
                .ToList();
        }
    }
}
