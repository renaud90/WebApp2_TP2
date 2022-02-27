using EvenementsAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public interface ICategoriesBL
    {
        public IEnumerable<CategorieDTO> GetList();
        public CategorieDTO Get(int id);

        public CategorieDTO Add(CategorieDTO value);
        public CategorieDTO Update(int id, CategorieDTO value);
        public void Delete(int id);
    }
}
