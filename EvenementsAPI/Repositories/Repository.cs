using EvenementsAPI.Data;
using EvenementsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntite 
    {
        private readonly EvenementsContext _dbContext;
        public Repository(EvenementsContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>()
                   .AsNoTracking()
                   .ToList();
        }
        public T GetById(int id)
        {
            return _dbContext.Set<T>()
                   .AsNoTracking()
                   .FirstOrDefault(_ => _.Id == id);
        }
        public int Add(T entite)
        {
            var e = _dbContext.Set<T>().Add(entite);
            _dbContext.SaveChanges();
            return e.Entity.Id;
        }
        public void Update(T entite) 
        {
            _dbContext.Set<T>().Update(entite);
            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var entite = GetById(id);
            _dbContext.Set<T>()
                      .Remove(entite);
            _dbContext.SaveChanges();
        }

    }
}
