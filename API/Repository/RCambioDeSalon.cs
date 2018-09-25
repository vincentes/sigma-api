using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class RCambioDeSalon : IRepository<CambioDeSalon>
    {
        private readonly SigmaContext _context;

        public RCambioDeSalon(SigmaContext context)
        {
            this._context = context;
        }

        public CambioDeSalon Add(CambioDeSalon item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            var excludeEscritoEntity = entityEntry.Entity;
            return excludeEscritoEntity;
        }

        public void Delete(CambioDeSalon item)
        {
            _context.Remove(item);
        }

        public IEnumerable<CambioDeSalon> GetAll()
        {
            return _context.CambioDeSalones
                    .Include(e => e.HoraMateriaFinal)
                        .ThenInclude(p => p.Grupo)
                            .ThenInclude(x => x.Orientacion)
                    .Include(e => e.HoraMateriaFinal)
                        .ThenInclude(p => p.Grupo)
                            .ThenInclude(x => x.Turno)
                    .Include(e => e.HoraMateriaInicial)
                    .Include(e => e.SalonCambio)
                    .Include(e => e.SalonInicial)
                    .Include(e => e.SalonFinal)
                    .ToList();
        }

        public CambioDeSalon GetById(int id)
        {
            return _context.CambioDeSalones
                    .Include(e => e.HoraMateriaFinal)
                        .ThenInclude(p => p.Grupo)
                            .ThenInclude(x => x.Orientacion)
                    .Include(e => e.HoraMateriaFinal)
                        .ThenInclude(p => p.Grupo)
                            .ThenInclude(x => x.Turno)
                    .Include(e => e.HoraMateriaInicial)
                    .Include(e => e.SalonCambio)
                    .Include(e => e.SalonInicial)
                    .Include(e => e.SalonFinal)
                    .SingleOrDefault(x => x.Id == id);
        }

        public void Update(CambioDeSalon item)
        {
            _context.Update<CambioDeSalon>(item);
            _context.SaveChanges();
        }
    }
}
