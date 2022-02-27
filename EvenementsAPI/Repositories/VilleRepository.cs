using EvenementsAPI.Data;
using EvenementsAPI.DTO;
using EvenementsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.Repositories
{
    public class VilleRepository : IVilleRepository
    {
        private readonly EvenementsContext _dbContext;
        public VilleRepository(EvenementsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Ville> GetAll()
        {
            return _dbContext.Set<Ville>()
                   .AsNoTracking()
                   .ToList();
        }
        public Ville GetById(int id)
        {
            return _dbContext.Set<Ville>()
                   .AsNoTracking()
                   .FirstOrDefault(_ => _.Id == id);
        }
        public int Add(Ville entite)
        {
            var e = _dbContext.Set<Ville>().Add(entite);
            _dbContext.SaveChanges();
            return e.Entity.Id;
        }
        public void Update(Ville entite)
        {
            _dbContext.Set<Ville>().Update(entite);
            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var entite = GetById(id);
            _dbContext.Set<Ville>()
                      .Remove(entite);
            _dbContext.SaveChanges();
        }
        public ICollection<VilleDTO> GetByNbEvenementsOrdered()
        {
            var counts = _dbContext.Set<Evenement>()
                                   .GroupBy(_ => _.VilleId, _ => _, (key, e) => new { Ville = GetById(key), Count = e.Count()})
                                   .OrderByDescending(_ => _.Count)
                                   .ToList();
            return counts.Select(_ => new VilleDTO {
                Id = _.Ville.Id,
                Nom = _.Ville.Nom,
                Region = _.Ville.Region
            }).ToList();
        }
    }
}
