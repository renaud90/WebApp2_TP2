using EvenementsAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public interface IEvenementsBL
    {
        public IEnumerable<EvenementDTO> GetList(int pageIndex, int pageCount, string filter);
        public EvenementDTO Get(int id);
        public IEnumerable<ParticipationDTO> GetParticipations(int id);
        public EvenementDTO Add(EvenementDTO value);
        public EvenementDTO Update(int id, EvenementDTO value);
        public void Delete(int id);
    }
}
