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
            //Classement des villes avec des événements associés
            var counts = _dbContext.Set<Evenement>()
                                   .GroupBy(_ => _.VilleId, _ => _, (key, e) => new { Id = key, Count = e.Count()})
                                   .OrderByDescending(_ => _.Count)
                                   .ToList();
            var villes = new List<Ville>();
            foreach(var c in counts)
            {
                villes.Add(GetById(c.Id));
            }
            //Ajout des villes avec aucun événement
            foreach(var v in _dbContext.Set<Ville>())
            {
                if (villes.FirstOrDefault(_ => _.Id == v.Id) == null)
                {
                    villes.Add(v);
                }
            }
            return villes.Select(_ => new VilleDTO {
                Id = _.Id,
                Nom = _.Nom,
                Region = _.Region
            }).ToList();
        }
    }
}
