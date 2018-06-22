using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class RGrupoDocente : IRepository<GrupoDocente>
    {
        private readonly SigmaContext _context;

        public RGrupoDocente(SigmaContext context)
        {
            _context = context;
        }

        public GrupoDocente Add(GrupoDocente item)
        {
            var grupoDocente = _context.Add(item);
            _context.SaveChanges();
            item.Id = grupoDocente.Entity.Id;
            return item;
        }

        public void Delete(GrupoDocente item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<GrupoDocente> GetAll()
        {
            return _context.GrupoDocente.ToList();
        }

        public GrupoDocente GetById(int id)
        {
            return _context.GrupoDocente.SingleOrDefault(x => x.Id == id);
        }

        public void Update(GrupoDocente item)
        {
            var grupoDocente = GetById(item.Id);
            grupoDocente.DocenteId = item.DocenteId;
            grupoDocente.GrupoId = item.GrupoId;
            _context.Update(grupoDocente);
            _context.SaveChanges();
        }
    }
}
