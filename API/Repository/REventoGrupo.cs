
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repository
{
    public class REventoGrupo : IRepository<EventoGrupo>
    {
        private readonly SigmaContext _context;

        public REventoGrupo(SigmaContext context)
        {
            _context = context;
        }

        public EventoGrupo Add(EventoGrupo item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            item.Id = entityEntry.Entity.Id;
            return item;
        }

        public void Delete(EventoGrupo item)
        {
            throw new NotImplementedException();
        }

        public void Update(EventoGrupo item)
        {
            EventoGrupo byId = GetById(item.Id);
            byId.Id = item.Id;
            byId.Grupo = item.Grupo;
            byId.Evento = item.Evento;
            byId.Notified = item.Notified;
            _context.Update(byId);
            _context.SaveChanges();
        }

        public IEnumerable<EventoGrupo> GetAll()
        {
            return _context.EventoGrupo
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
                 .Include(t => t.Grupo)
                    .ThenInclude(e => e.Alumnos)
                        .ThenInclude(t => t.Token)
                .Include(t => t.Evento)
                .ToList();
        }

        public EventoGrupo GetById(int id)
        {
            return _context.EventoGrupo
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
                 .Include(t => t.Grupo)
                    .ThenInclude(e => e.Alumnos)
                        .ThenInclude(t => t.Token)
                .Include(t => t.Evento)
                .SingleOrDefault(x => x.Id == id);
        }
    }
}
