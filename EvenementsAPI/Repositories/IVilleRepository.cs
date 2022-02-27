using EvenementsAPI.DTO;
using EvenementsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.Repositories
{
    public interface IVilleRepository : IRepository<Ville>
    {
        public ICollection<VilleDTO> GetByNbEvenementsOrdered();
    }
}
