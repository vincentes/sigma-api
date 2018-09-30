using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data;

namespace API.Repository
{
    public class REncuestaGlobal : IRepository<EncuestaGlobal>
    {
        private readonly SigmaContext _context;

        public REncuestaGlobal(SigmaContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll<T>(int encuestaId) where T : Pregunta
        {
            IEnumerable<T> result;
            var query = _context.Set<Pregunta>().OfType<T>().Where(e => e.EncuestaId == encuestaId);
            foreach (var nav in _context.Model.FindEntityType(typeof(T)).GetNavigations())
            {
                query = query.Include(nav.Name);
            }
            return query.ToList();
        }

        public EncuestaGlobal Add(EncuestaGlobal item)
        {
            var encuesta = _context.Add(item);
            _context.SaveChanges();
            item.Id = encuesta.Entity.Id;
            return item;
        }

        public void Delete(EncuestaGlobal item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<EncuestaGlobal> GetAll()
        {
            return _context.EncuestasGlobales
                .Include(p => p.Preguntas)
                    .ThenInclude(e => (e as PreguntaMO).Respuestas)
                .Include(p => p.Preguntas)
                    .ThenInclude(e => (e as PreguntaMO).Opciones)
                        .ThenInclude(g => g.RespuestasAsociadas)
                            .ThenInclude(d => d.Alumno)
                  .Include(p => p.Preguntas)
                    .ThenInclude(e => (e as PreguntaMO).Opciones)
                        .ThenInclude(g => g.OpcionRespuestas)
                .Include(p => p.Preguntas)
                    .ThenInclude(e => (e as PreguntaUO).Respuestas)
                .Include(p => p.Preguntas)
                    .ThenInclude(e => (e as PreguntaUO).Opciones)
                .Include(p => p.Preguntas)
                    .ThenInclude(e => (e as PreguntaEL).Respuestas);
        }

        public EncuestaGlobal GetById(int id)
        {
            return _context.EncuestasGlobales
                .Include(e => e.Preguntas)
                 .SingleOrDefault(e => e.Id == id);
        }



        public void Update(EncuestaGlobal item)
        {
            var encuesta = GetById(item.Id);
            encuesta.Preguntas = item.Preguntas;
            _context.Update(encuesta);
            _context.SaveChanges();
        }

    }
}
