using EvenementsAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public interface IVillesBL
    {
        public IEnumerable<VilleDTO> GetList();
        public VilleDTO Get(int id);
        public IEnumerable<EvenementDTO> GetEvenements(int id);

        public VilleDTO Add(VilleDTO value);
        public VilleDTO Updade(int id, VilleDTO value);

        public void Delete(int id);
    }
}
