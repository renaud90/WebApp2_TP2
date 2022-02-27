using EvenementsAPI.Data;
using EvenementsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.Repositories
{
    public class EvenementRepository : IEvenementRepository
    {
        private readonly EvenementsContext _dbContext;
        public EvenementRepository(EvenementsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Evenement> GetAll()
        {
            return _dbContext.Set<Evenement>()
                   .AsNoTracking()
                   .ToList();
        }
        public Evenement GetById(int id)
        {
            return _dbContext.Set<Evenement>()
                   .AsNoTracking()
                   .FirstOrDefault(_ => _.Id == id);
        }
        public int Add(Evenement entite)
        {
            var e = _dbContext.Set<Evenement>().Add(entite);
            _dbContext.SaveChanges();
            return e.Entity.Id;
        }
        public void Update(Evenement entite)
        {
            _dbContext.Set<Evenement>().Update(entite);
            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var entite = GetById(id);
            _dbContext.Set<Evenement>()
                      .Remove(entite);
            _dbContext.SaveChanges();
        }

        public double GetTotalVentes(int id)
        {
            var prix = GetById(id).Prix;
            return _dbContext.Set<Participation>()
                             .AsNoTracking()
                             .Where(_ => _.EvenementId == id)
                             .Sum(_ => _.NbPlaces * prix);
            
        }
    }
}
