using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class RGrupo : IRepository<Grupo>
    {
        private readonly SigmaContext _context;

        public RGrupo(SigmaContext context)
        {
            _context = context;
        }

        public Grupo Add(Grupo item)
        {
            var grupo = _context.Add(item);
            _context.SaveChanges();
            item.Id = grupo.Entity.Id;
            return item;
        }

        public void Delete(Grupo item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<Grupo> GetAll()
        {
            return _context.Grupos.ToList();
        }

        public Grupo GetById(int id)
        {
            return _context.Grupos.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Grupo item)
        {
            var grupo = GetById(item.Id);
            grupo.Id = item.Id;
            grupo.Grado = item.Grado;
            grupo.OrientacionId = item.OrientacionId;
            grupo.TurnoId = item.TurnoId;
            grupo.Numero = item.Numero;
            _context.Update(grupo);
            _context.SaveChanges();
        }
    }
}
