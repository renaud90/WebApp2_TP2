using EvenementsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.Repositories
{
    public interface IEvenementRepository : IRepository<Evenement>
    {
        public double GetTotalVentes(int id);
    }
}
